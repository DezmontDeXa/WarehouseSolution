using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;
using CameraListenerService;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;

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
        private readonly IBarriersService barriersService;
        private bool _awaitNextBlock = false;
        private CameraNotifyBlock _anprBlock;
        private CameraNotifyBlock _pictureBlock;
        private CarState _errorState;

        public CameraRoleBase(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService)
        {
            RoleName = GetType().Name;
            Logger = logger;
            _waitingListsService = waitingListsService;
            this.barriersService = barriersService;
            using (var db = new WarehouseContext())
                _errorState = db.CarStates.First(x => x.TypeName == nameof(ErrorState));
        }

        public void AddThatRoleToDB(WarehouseContext db)
        {
            var existRole = db.CameraRoles.FirstOrDefault(x => x.TypeName == RoleName);
            if (existRole == null)
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = RoleName });
            else
            {
                existRole.Description = Description;
                existRole.Name = Name;
            }
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
                plateNumber = TransliterateToRu(plateNumber);
                var direction = GetDirection(_anprBlock);
                Logger.Trace($"{camera.Name}: Обнаружена машина ({plateNumber}). Направление: {direction}");

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


        protected void ChangeStatus(Camera camera, Car car, CarState status)
        {
            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.CarStateId = status.Id;
                db.SaveChanges();
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить статус на \"{status.Name}\"");
            }
        }

        protected CarState SetErrorStatus(Camera camera, Car car)
        {
            ChangeStatus(camera, car, _errorState);
            return _errorState;
        }

        protected void SetCarArea(Camera camera, Car car, Area area)
        {
            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.AreaId = area?.Id;
                db.SaveChanges();
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить территорию на \"{area?.Name ?? "Вне системы"}\"");
            }
        }

        protected void SetCarTargetArea(Camera camera, Car car, Area area)
        {
            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == car.Id);
                carInDb.TargetAreaId = area.Id;
                db.SaveChanges();
                Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) сменить целевую территорию на \"{area.Name}\"");
            }
        }

        protected void OpenBarrier(Camera camera, Car car)
        {
            //TODO: Открыть шлагбаум
            //barriersService.Switch()
            Logger.Trace($"{camera.Name}:\t Для машины ({car.PlateNumberForward}) открыть шлагбаум");
        }

        protected static CarState GetDbCarStateByType<T>(WarehouseContext db) where T : CarStateBase
        {
            return db.CarStates.ToList().First(x => CarStateBase.Equals<T>(x));
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


        private static string TransliterateToRu(string input)
        {
            var ru = "АВЕКМНОРСТУХ";
            var en = "ABEKMHOPCTYX";

            var inputArray = input.ToCharArray();
            for (var i = 0; i < inputArray.Length; i++)
            {
                var ch = input[i];
                if (en.Contains(ch))
                    inputArray[i] = ru[en.IndexOf(ch)];
            }

            return string.Join("", inputArray);
        }

        private static bool IsAnprEvent(CameraNotifyBlock notifyBlock)
        {
            if (notifyBlock.Headers["Content-Type"] == "application/xml" || notifyBlock.Headers["Content-Type"] == "text/xml")
                // Skip all events,except Car detected
                if (notifyBlock.EventType == "ANPR")
                    return true;

            return false;
        }

        private static string GetPlateNumber(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        private static string GetDirection(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
        }

        protected void SendUnknownCarNotify(Camera camera, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            var notify = new UnknownCarNotify()
            {
                Camera = camera,
                CreatedOn = DateTime.Now,
                DetectedPlateNumber = plateNumber,
                Direction = direction,
                PlateNumberPicture = pictureBlock.ContentBytes,
            };

            using (var db = new WarehouseContext())
            {
                var cameraRole = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                notify.Role = cameraRole;

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
                var cameraRole = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                notify.Role = cameraRole;

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
                var cameraRole = db.CameraRoles.First(x => x.TypeName == this.GetType().Name);
                notify.Role = cameraRole;

                db.ExpiredListCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }

        protected void SendInspectionRequiredCarNotify(Car car)
        {
            var notify = new InspectionRequiredCarNotify()
            {                
                CreatedOn = DateTime.Now,
                Car = car,
            };

            using (var db = new WarehouseContext())
            {
                db.InspectionRequiredCarNotifies.Add(notify);
                db.SaveChanges();
            }
        }
    }
}
