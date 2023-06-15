using NLog;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.WaitingListServices;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.AppSettings;
using Warehouse.ConfigDataBase;

namespace Warehouse.CameraRoles
{
    public abstract class CameraRoleBase : ICameraRoleBase
    {
        private IWaitingListsService _waitingListsService;
        private IBarriersService _barriersService;
        private IRussificationService _ruService;
        private IAppSettings _settings;
        private IWarehouseDataBaseMethods _dbMethods;
        private bool _awaitNextBlock = false;
        private ICameraNotifyBlock _anprBlock;
        private ICameraNotifyBlock _pictureBlock;
        private List<int> _expectedStateIds;

        public int Id { get; protected init; }
        public string Name { get; protected init; }
        public string Description { get; protected init; }
        protected ILogger Logger { get; private set; }
        protected IReadOnlyList<int> ExpectedStateIds
        {
            get => _expectedStateIds;
        }

        public CameraRoleBase(ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService, IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
        {
            Logger = logger;
            _waitingListsService = waitingListsService;
            _barriersService = barriersService;
            _ruService = ruService;
            _settings = settings;
            _dbMethods = dbMethods;

            _expectedStateIds = new List<int>();
        }

        public void Execute(ICamera camera, ICameraNotifyBlock notifyBlock)
        {
            try
            {
                // семафор для сбора двух блоков - один с данными ANPR другой с фоткой
                if (IsAnprEvent(notifyBlock))
                {
                    _awaitNextBlock = true;
                    _anprBlock = notifyBlock;
                    return;
                }
                if (!_awaitNextBlock) return;
                _awaitNextBlock = false;
                _pictureBlock = notifyBlock;

                var plateNumber = _ruService.ToRu(ParsePlateNumber(_anprBlock));
                var direction = ParseDirection(_anprBlock);

                ProcessDetectedCar(camera, plateNumber, direction);
            }
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {Name}. Ex: {ex}");
                return;
            }
        }

        #region Virtual methods

        protected virtual void OnCarNotFound(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock _pictureBlock, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}:\t Обнаружена неизвестная машина с номером ({plateNumber}).");
        }

        protected virtual void OnCarNotInLists(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock _pictureBlock, ICar car, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}:\t Обнаружена машина с номером ({plateNumber}) не из списков.");
        }

        protected virtual void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            Logger.Trace($"{camera.Name}:\t Прибыла машина из постоянного списка \"{info.TopAccessTypeList?.Name}\" с номером ({info.Car.PlateNumberForward}).");
        }

        protected virtual void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            Logger.Trace($"{camera.Name}:\t Прибыла машина из временного списка \"{info.TopAccessTypeList?.Name}\" с номером ({info.Car.PlateNumberForward}).");
        }

        #endregion

        #region Protected methods

        protected void ChangeCarStatus(ICamera camera, int carId, int stateId)
        {
            ICar? car = null;
            ICarStateType? state = null;
            try
            {
                car = _dbMethods.GetCarById(carId);
                state = _dbMethods.GetStateById(stateId);
                _dbMethods.SetCarState(carId, stateId);
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить статус на \"{state.Name}\"");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{camera.Name}:\t Не удалось сменить статус машины ({car?.PlateNumberForward}, {state?.Name}). {ex.Message}");
                return;
            }
        }

        protected void SetCarErrorStatus(ICamera camera, int carId)
        {
            ChangeCarStatus(camera, carId, new ErrorState().Id);
        }

        protected void SetCarArea(ICamera camera, int carId, int? areaId)
        {
            using (var configsDb = new WarehouseConfig(_settings))
            {
                var car = _dbMethods.GetCarById(carId);
                _dbMethods.SetCarArea(car, areaId);

                IArea? area = null;
                if (areaId != null)
                    area = configsDb.Areas.Find(areaId);

                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected void SetCarTargetArea(ICamera camera, int carId, int? areaId)
        {
            using (var configsDb = new WarehouseConfig(_settings))
            {
                var car = _dbMethods.GetCarById(carId);
                _dbMethods.SetCarTargetArea(car, areaId);

                IArea? area = null;
                if (areaId != null)
                    configsDb.Areas.Find(areaId);

                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить целевую территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected IArea? GetCameraArea(ICamera camera)
        {
            using (var configsDb = new WarehouseConfig(_settings))
                return configsDb.Areas.Find(camera.AreaId);
        }

        protected IArea? GetCarTargetArea(ICar car)
        {
            using (var configsDb = new WarehouseConfig(_settings))
                return configsDb.Areas.Find(car.TargetAreaId);
        }

        protected void OpenBarrier(ICamera camera, ICar car)
        {
            using (var configsDb = new WarehouseConfig(_settings))
            {
                var info = configsDb.BarrierInfos.FirstOrDefault(x => x.AreaId == camera.AreaId);
                _barriersService.Open(info);
            }
            Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) открыть шлагбаум");
        }

        protected void AddExpectedState(ICarStateBase state)
        {
            _expectedStateIds.Add(state.Id);
        }

        protected bool InvalideWaitingCamera(ICamera camera, ICarAccessInfo info, ICar car, IArea cameraArea)
        {
            if (info.TopAccessTypeList?.Camera != camera.Name)
            {
                Logger.Error($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {info.TopAccessTypeList?.Camera}, но прибыла на {cameraArea?.Name}. ");
                return true;
            }

            return false;
        }

        protected IArea? GetNaisArea()
        {
            using (var configsDb = new WarehouseConfig(_settings))
            {
                var value = configsDb.Configs.First(x => x.Key == "NaisAreaId")?.Value;
                var areaId = int.Parse(value);
                return configsDb.Areas.Find(areaId);
            }
        }

        #endregion

        #region Private methods

        private void ProcessDetectedCar(ICamera camera, string plateNumber, string direction)
        {
            Logger.Trace($"{camera.Name}: Обнаружена машина ({plateNumber}). Направление: {direction}");

            if (!IsAvailableDirection(camera, direction))
                return;

            var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

            if (!ValidateDetectedCar(carAccessInfo, camera, plateNumber, direction))
                return;

            _dbMethods.SendCarDetectedNotify(camera, carAccessInfo);

            switch (carAccessInfo.TopAccessType)
            {
                case AccessGrantType.Free:
                    OnCarWithFreeAccess(camera, carAccessInfo, _pictureBlock);
                    break;
                case AccessGrantType.Tracked:
                    OnCarWithTempAccess(camera, carAccessInfo, _pictureBlock);
                    break;
            }
        }

        private bool ValidateDetectedCar(ICarAccessInfo info, ICamera camera, string plateNumber, string direction)
        {
            if (info.Car == null)
            {
                OnCarNotFound(camera, _anprBlock, _pictureBlock, plateNumber, direction);
                return false;
            }

            if (info.AllIncludeLists == null)
            {
                OnCarNotInLists(camera, _anprBlock, _pictureBlock, info.Car, plateNumber, direction);
                return false;
            }

            if (_expectedStateIds != null && _expectedStateIds.Count > 0 && !_expectedStateIds.Exists(x => x == info.Car?.CarStateId))
            {
                var carState = _dbMethods.GetStateById(info.Car.CarStateId.Value);
                Logger.Warn($"{camera.Name}:\t Машина ({info.Car.PlateNumberForward}) имела неожиданный статус. Текущий статус: \"{carState.Name}\".");
                return IfNotExpectedCarState(carState, _expectedStateIds);
            }

            return true;
        }


        private bool IsAvailableDirection(ICamera camera, string direction)
        {
            if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
            {
                Logger.Trace($"{camera.Name}:\t Направление: {direction}. Ожидалось \"{camera.Direction}\". Без действий.");
                return false;
            }
            return true;
        }

        private static bool IsAnprEvent(ICameraNotifyBlock notifyBlock)
        {
            if (notifyBlock.Headers["Content-Type"] == "application/xml" || notifyBlock.Headers["Content-Type"] == "text/xml")
                // Skip all events,except Car detected
                if (notifyBlock.EventType == "ANPR")
                    return true;

            return false;
        }

        private static string ParsePlateNumber(ICameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        private static string ParseDirection(ICameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
        }

        protected void SendUnknownCarNotify(ICamera camera, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            _dbMethods.SendUnknownCarNotify(camera, pictureBlock, plateNumber, direction);
        }

        protected void SendNotInListCarNotify(ICamera camera, ICar car, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            _dbMethods.SendNotInListCarNotify(camera, car, pictureBlock, plateNumber, direction);
        }
        protected void SendNotInListCarNotify(ICamera camera, ICar car, IWaitingList waitingList, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            _dbMethods.SendExpriredListCarNotify(camera, car, waitingList, pictureBlock, plateNumber, direction);
        }

        protected void SendInspectionRequiredCarNotify(ICar car)
        {
            _dbMethods.SendInspectionRequiredCarNotify(car);
        }
        protected virtual bool IfNotExpectedCarState(ICarStateType carState, List<int> expectedStateIds)
        {
            return false;
        }

        #endregion


    }
}
