using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;

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
                // Skip all events,except Car detected
                if (notifyBlock.EventType != "ANPR")
                    return;

                var plateNumber = GetPlateNumber(notifyBlock);
                var direction = GetDirection(notifyBlock);
                Logger.Info($"{camera.Name}: Обнаружена машина ({plateNumber}). Направление: {direction}");

                if (!IsAvailableDirection(camera, direction)) return;

                var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

                if(carAccessInfo.Car == null)
                {
                    OnCarNotFound(camera, notifyBlock, plateNumber, direction);
                    return;
                }

                if (carAccessInfo.List == null)
                {
                    OnCarNotInLists(camera, notifyBlock, carAccessInfo.Car, plateNumber, direction);
                    return;
                }

                if (ExpectedStates != null && ExpectedStates.Count > 0 && ExpectedStates.Exists(x => x.Id== carAccessInfo.Car?.CarState?.Id)) 
                {
                    Logger.Warn($"{camera.Name}: Машина ({carAccessInfo.Car.PlateNumberForward}) имела неожиданный статус. Текущий статус: \"{carAccessInfo.Car.CarState.Name} на {camera.Area.Name}\". Без действий.");
                    return;
                }

                switch (carAccessInfo.AccessType)
                {
                    case AccessGrantType.Free:
                        OnCarWithFreeAccess(camera, carAccessInfo.Car, carAccessInfo.List);
                        break;
                    case AccessGrantType.Tracked:
                        OnCarWithTempAccess(camera, carAccessInfo.Car, carAccessInfo.List);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {RoleName}. Ex: {ex}");
                return;
            }
        }


        protected virtual void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}: Обнаружена незарегистрированная машина ({plateNumber}).");
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }

        protected virtual void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, Car car, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}: Обнаружена машина ({plateNumber}) не из списков.");
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }

        protected virtual void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            Logger.Info($"{camera.Name}: Прибыла машина из постоянного списка {list.Name} с номером ({car.PlateNumberForward}).");
            //TODO: Открыть шлагбаум. Сменить статус  \"На въезде\"
        }

        protected virtual void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            Logger.Info($"{camera.Name}: Прибыла машина из временного списка {list.Name} с номером ({car.PlateNumberForward}).");
        }


        private bool IsAvailableDirection(Camera camera, string direction)
        {
            if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
            {
                Logger.Warn($"{camera.Name}: Направление: {direction}. Ожидалось \"{camera.Direction}\". Без действий.");
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

        protected void ChangeStatus(Camera camera, Car car, CarState status)
        {
            using (var db = new WarehouseContext())
            {
                Logger.Info($"{camera.Name}: Для машины ({car.PlateNumberForward}) сменить статус на \"{status.Name}\"");
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.CarState = status;
                db.SaveChangesAsync();
            }
        }
        protected void ChangeStatus(Camera camera, Car car,  Func<WarehouseContext, Camera, Car, CarState> getCarStateFunc)
        {
            using (var db = new WarehouseContext())
            {
                var status = getCarStateFunc.Invoke(db, camera, car);

                Logger.Info($"{camera.Name}: Для машины ({car.PlateNumberForward}) сменить статус на \"{status.Name}\"");
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.CarState = status;
                db.SaveChangesAsync();
            }
        }

        protected void OpenBarrier(Camera camera, Car car)
        {
            //TODO: Открыть шлагбаум
        }

    }
}
