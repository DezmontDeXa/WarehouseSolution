using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;
using CameraListenerService;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using SharedLibrary.Extensions;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CameraRoleBase
    {
        private readonly WaitingListsService _waitingListsService;
        private readonly IBarriersService _barriersService;
        private bool _awaitNextBlock = false;
        private CameraNotifyBlock _anprBlock;
        private CameraNotifyBlock _pictureBlock;
        private List<int> _expectedStateIds;

        public int Id { get; protected init; }
        public string Name { get; protected init; }
        public string Description { get; protected init; }
        protected ILogger Logger { get; private set; }
        protected IReadOnlyList<int> ExpectedStateIds
        {
            get => _expectedStateIds;
        }

        public CameraRoleBase(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService)
        {
            Logger = logger;
            _waitingListsService = waitingListsService;
            _barriersService = barriersService;
            _expectedStateIds = new List<int>();
        }

        public void AddThatRoleToDB(WarehouseContext db)
        {
            var existRole = db.CameraRoles.Find(Id);
            if (existRole == null)
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = this.GetType().Name });
            else
            {
                existRole.Description = Description;
                existRole.Name = Name;
                existRole.TypeName = this.GetType().Name;
            }
        }

        public void Execute(Camera camera, CameraNotifyBlock notifyBlock)
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

                var plateNumber = StringExtensions.TransliterateToRu(ParsePlateNumber(_anprBlock));
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

        protected virtual void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock _pictureBlock, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}:\t Обнаружена неизвестная машина с номером ({plateNumber}).");
        }

        protected virtual void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock _pictureBlock, Car car, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}:\t Обнаружена машина с номером ({plateNumber}) не из списков.");
        }

        protected virtual void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            Logger.Trace($"{camera.Name}:\t Прибыла машина из постоянного списка \"{list.Name}\" с номером ({car.PlateNumberForward}).");
        }

        protected virtual void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            Logger.Trace($"{camera.Name}:\t Прибыла машина из временного списка \"{list.Name}\" с номером ({car.PlateNumberForward}).");
        }

        #endregion

        #region Protected methods

        protected void ChangeStatus(Camera camera, int carId, int stateId)
        {
            Car? car = null;
            CarState? state = null;
            try
            {
                using (var db = new WarehouseContext())
                {
                    car = db.Cars.Find(carId);
                    state = db.CarStates.Find(stateId);
                    car.CarStateId = stateId;
                    db.SaveChanges();

                    Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить статус на \"{state.Name}\"");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"{camera.Name}:\t Не удалось сменить статус машины ({car?.PlateNumberForward}, {state?.Name}). {ex.Message}");
                return;
            }
        }

        protected void SetErrorStatus(Camera camera, int carId)
        {
            ChangeStatus(camera, carId, new ErrorState().Id);
        }

        protected void SetCarArea(Camera camera, int carId, int? areaId)
        {
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.Find(carId);
                car.AreaId = areaId;

                Area? area = null;
                if (areaId != null)
                    db.Areas.Find(areaId);

                db.SaveChanges();

                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected void SetCarTargetArea(Camera camera, int carId, int? areaId)
        {
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.Find(carId);
                car.TargetAreaId = areaId;

                Area? area = null;
                if (areaId != null)
                    db.Areas.Find(areaId);

                db.SaveChanges();
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить целевую территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected void OpenBarrier(Camera camera, Car car)
        {
            using(var db = new WarehouseContext())
            {
                var info = db.BarrierInfos.FirstOrDefault(x => x.AreaId == camera.AreaId);
                _barriersService.Open(info);
            }
            Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) открыть шлагбаум");
        }

        protected void AddExpectedState(CarStateBase state)
        {
            _expectedStateIds.Add(state.Id);
        }

        protected static Area? GetCameraArea(Camera camera)
        {
            using (var db = new WarehouseContext())
                return db.Areas.Find(camera.AreaId);
        }

        protected static Area? GetCarTargetArea(Car car)
        {
            using (var db = new WarehouseContext())
                return db.Areas.Find(car.TargetAreaId);
        }

        #endregion

        #region Private methods

        private void ProcessDetectedCar(Camera camera, string plateNumber, string direction)
        {
            Logger.Trace($"{camera.Name}: Обнаружена машина ({plateNumber}). Направление: {direction}");

            if (!IsAvailableDirection(camera, direction)) 
                return;

            var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

            if (!ValidateDetectedCar(carAccessInfo, camera, plateNumber, direction))
                return;

            SendCarDetectedNotify(camera, carAccessInfo);

            switch (carAccessInfo.AccessType)
            {
                case AccessGrantType.Free:
                    OnCarWithFreeAccess(camera, carAccessInfo.Car, carAccessInfo.List, _pictureBlock);
                    break;
                case AccessGrantType.Tracked:
                    OnCarWithTempAccess(camera, carAccessInfo.Car, carAccessInfo.List, _pictureBlock);
                    break;
            }
        }

        private bool ValidateDetectedCar(CarAccessInfo info, Camera camera, string plateNumber, string direction)
        {
            if (info.Car == null)
            {
                OnCarNotFound(camera, _anprBlock, _pictureBlock, plateNumber, direction);
                return false;
            }

            if (info.List == null)
            {
                OnCarNotInLists(camera, _anprBlock, _pictureBlock, info.Car, plateNumber, direction);
                return false;
            }

            if (_expectedStateIds != null && _expectedStateIds.Count > 0 && !_expectedStateIds.Exists(x => x == info.Car?.CarStateId))
            {
                using (var db = new WarehouseContext())
                {
                    var carState = db.CarStates.Find(info.Car.CarStateId);
                    Logger.Warn($"{camera.Name}:\t Машина ({info.Car.PlateNumberForward}) имела неожиданный статус. Текущий статус: \"{carState.Name}\". Без действий.");
                    return false;
                }
            }

            return true;
        }

        private bool IsAvailableDirection(Camera camera, string direction)
        {
            if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
            {
                Logger.Trace($"{camera.Name}:\t Направление: {direction}. Ожидалось \"{camera.Direction}\". Без действий.");
                return false;
            }
            return true;
        }

        private static bool IsAnprEvent(CameraNotifyBlock notifyBlock)
        {
            if (notifyBlock.Headers["Content-Type"] == "application/xml" || notifyBlock.Headers["Content-Type"] == "text/xml")
                // Skip all events,except Car detected
                if (notifyBlock.EventType == "ANPR")
                    return true;

            return false;
        }

        private static string ParsePlateNumber(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        private static string ParseDirection(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
        }

        #endregion

        #region Send Notifies

        private void SendCarDetectedNotify(Camera camera, CarAccessInfo carAccessInfo)
        {
            using (var db = new WarehouseContext())
            {
                db.CarDetectedNotifies.Add(new CarDetectedNotify()
                {
                    Camera = db.Cameras.Find(camera.Id),
                    Car = db.Cars.Find(carAccessInfo.Car.Id),
                    CreatedOn = DateTime.Now,
                });
                db.SaveChanges();
            }
        }

        protected void SendUnknownCarNotify(Camera camera, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new UnknownCarNotify()
            {
                CreatedOn = DateTime.Now,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
            };

            using (var db = new WarehouseContext())
            {
                notify.Camera = db.Cameras.Find(camera.Id);
                notify.Role = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                db.UnknownCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }

        protected void SendNotInListCarNotify(Camera camera, Car car, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new NotInListCarNotify()
            {
                Camera = camera,
                CreatedOn = DateTime.Now,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
                Car = car,
            };

            using (var db = new WarehouseContext())
            {
                notify.Role = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                db.NotInListCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }

        protected void SendExpriredListCarNotify(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new ExpiredListCarNotify()
            {
                Camera = camera,
                CreatedOn = DateTime.Now,
                WaitingList = list,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
                Car = car,
            };

            using (var db = new WarehouseContext())
            {
                notify.Role = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                db.ExpiredListCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }

        protected void SendInspectionRequiredCarNotify(Car car)
        {
            var notify = new InspectionRequiredCarNotify()
            {
                CreatedOn = DateTime.Now,
            };

            using (var db = new WarehouseContext())
            {
                notify.Car = db.Cars.Find(car.Id);
                db.InspectionRequiredCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }

        #endregion

    }
}
