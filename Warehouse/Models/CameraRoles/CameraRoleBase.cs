using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;
using CameraListenerService;
using Warehouse.Models.CarStates;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CameraRoleBase
    {
        public string RoleName { get; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected ILogger Logger { get; private set; }
        protected List<CarState> ExpectedStates { get; set; }

        private readonly WaitingListsService _waitingListsService;
        private bool _awaitNextBlock = false;
        private CameraNotifyBlock _anprBlock;
        private CameraNotifyBlock _pictureBlock;


        public CameraRoleBase(ILogger logger, WaitingListsService waitingListsService)
        {
            RoleName = GetType().Name;
            Logger = logger;
            _waitingListsService = waitingListsService;
        }

        public void AddThatRoleToDB(WarehouseContext db)
        {
            if (!db.CameraRoles.Any(x => x.TypeName == RoleName))
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = RoleName });
        }

        public void Execute(Camera camera, CameraNotifyBlock notifyBlock)
        {
            try
            {

                if (IsAnprEvent(notifyBlock))
                {
                    _awaitNextBlock = true;
                    _anprBlock = notifyBlock;
                    return;
                }

                if (_awaitNextBlock)
                {
                    _awaitNextBlock = false;
                    _pictureBlock = notifyBlock;
                }
                else
                    return;

                var plateNumber = GetPlateNumber(_anprBlock);
                var direction = GetDirection(_anprBlock);
                Logger.Info($"{camera.Name}: Обнаружена машина ({plateNumber}). Направление: {direction}");

                if (!IsAvailableDirection(camera, direction)) return;

                var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

                if (carAccessInfo.Car == null)
                {
                    OnCarNotFound(camera, _anprBlock, _pictureBlock, plateNumber, direction);
                    return;
                }

                if (carAccessInfo.List == null)
                {
                    OnCarNotInLists(camera, _anprBlock, _pictureBlock, carAccessInfo.Car, plateNumber, direction);
                    return;
                }

                if (ExpectedStates != null && ExpectedStates.Count > 0 && !ExpectedStates.Exists(x => x.Id == carAccessInfo.Car?.CarState?.Id))
                {
                    Logger.Warn($"{camera.Name}:\t Машина ({carAccessInfo.Car.PlateNumberForward}) имела неожиданный статус. Текущий статус: \"{carAccessInfo.Car.CarState.Name} на {camera.Area.Name}\". Без действий.");
                    return;
                }

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
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {RoleName}. Ex: {ex}");
                return;
            }
        }

        private bool IsAnprEvent(CameraNotifyBlock notifyBlock)
        {
            if (notifyBlock.Headers["Content-Type"] == "application/xml" || notifyBlock.Headers["Content-Type"] == "text/xml")
                // Skip all events,except Car detected
                if (notifyBlock.EventType == "ANPR")
                    return true;

            return false;
        }

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
            Logger.Info($"{camera.Name}:\t Прибыла машина из постоянного списка \"{list.Name}\" с номером ({car.PlateNumberForward}).");
        }

        protected virtual void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            Logger.Info($"{camera.Name}:\t Прибыла машина из временного списка \"{list.Name}\" с номером ({car.PlateNumberForward}).");
        }


        protected void ChangeStatus(Camera camera, Car car, CarState status)
        {
            using (var db = new WarehouseContext())
            {
                Logger.Info($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить статус на \"{status.Name}\"");
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.CarStateId = status.Id;
                db.SaveChanges();
            }
        }

        protected void SetCarArea(Camera camera, Car car, Area area)
        {
            using (var db = new WarehouseContext())
            {
                Logger.Info($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить территорию на \"{area.Name}\"");
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.AreaId = area.Id;
                db.SaveChanges();
            }
        }

        protected void OpenBarrier(Camera camera, Car car)
        {
            Logger.Info($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) открыть шлагбаум");
            //TODO: Открыть шлагбаум
        }

        protected static CarState GetDbCarStateByType<T>(WarehouseContext db) where T : CarStateBase
        {
            return db.CarStates.ToList().First(x => CarStateBase.Equals<T>(x));
        }


        private bool IsAvailableDirection(Camera camera, string direction)
        {
            if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
            {
                Logger.Warn($"{camera.Name}:\t Направление: {direction}. Ожидалось \"{camera.Direction}\". Без действий.");
                return false;
            }
            return true;
        }

        private string GetPlateNumber(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        private string GetDirection(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
            //return block.XmlDocument.SelectSingleNode("//EventNotificationAlert/ANPR/direction")?.InnerText;
        }

    }
}
