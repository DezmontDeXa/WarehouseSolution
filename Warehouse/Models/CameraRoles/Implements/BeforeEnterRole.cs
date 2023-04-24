using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        private readonly WarehouseContext _db;
        private readonly WaitingListsService _waitingListsService;

        public BeforeEnterRole(ILogger logger, WarehouseContext db, WaitingListsService waitingListsService) : base(logger)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";
            this._db = db;
            _waitingListsService = waitingListsService;
        }

        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock)
        {
            var plateNumber = GetPlateNumber(notifyBlock);
            var direction = GetDirection(notifyBlock);
            Logger.Info($"{camera.Name}: Обнаружена машина ({plateNumber})");

            if(direction != "forward")
            {
                Logger.Info($"{camera.Name}: Направление: {direction}. Ожидалось \"forward\". Без действий.");
                return;
            }

            var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

            switch (carAccessInfo.AccessType)
            {
                case AccessGrantType.None:
                    Logger.Info($"{camera.Name}: Car {plateNumber} not awaited. Send notify for checkpoint.");
                    ProcessNotTrakedCar(camera, plateNumber, carAccessInfo);
                    break;
                case AccessGrantType.Free:
                    Logger.Info($"{camera.Name}: Car {plateNumber} in constant list. Open barrier.");
                    ProcessFreeCar(camera, plateNumber, carAccessInfo);
                    break;
                case AccessGrantType.Tracked:
                    ProcessTrackedCar(camera, plateNumber, carAccessInfo);
                    break;
            }
        }

        private void ProcessFreeCar(Camera camera, string plateNumber, CarAccessInfo carAccessInfo)
        {
            Logger.Info($"{camera.Name}: Прибыла машина из списка {carAccessInfo.List.Name} с номером ({plateNumber}). Открыть шлагбаум. Сменить статус \"На въезде\"");
            OpenBarrier(camera, carAccessInfo);
            ChangeCarStatusToEnterOnCameraArea(camera, carAccessInfo);
        }

        private void ProcessNotTrakedCar(Camera camera, string plateNumber, CarAccessInfo carAccessInfo)
        {
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }

        private void ProcessTrackedCar(Camera camera, string plateNumber, CarAccessInfo carAccessInfo)
        {
            if(carAccessInfo.Car.State == null)
            {
                Logger.Warn($"{camera.Name}: Машина {plateNumber} не имеет статуса. Без действий.");
                return;
            }

            // Если машина ожидается на территории камеры
            if(carAccessInfo.Car.State.Name == "Ожидается" && carAccessInfo.Car.State.Area == camera.Area)
            {
                Logger.Info($"{camera.Name}: Прибыла машина из списка {carAccessInfo.List.Name} с номером ({plateNumber}). Открыть шлагбаум. Сменить статус \"На въезде\"");
                OpenBarrier(camera, carAccessInfo);
                ChangeCarStatusToEnterOnCameraArea(camera, carAccessInfo);
            }
            else
            {
                Logger.Warn($"{camera.Name}: Машина ({plateNumber}) имела неожиданный статус. Ожидаемый статус: Ожидается на {camera.Area.Name}. Текущий статус: {carAccessInfo.Car.State.Name}. Без действий.");
                return;
            }

        }

        private void OpenBarrier(Camera camera, CarAccessInfo carAccessInfo)
        {
            //TODO: Открыть шлагбаум
        }

        /// <summary>
        /// Сменить статус машины на "На въезде" в территорию камеры
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="carAccessInfo"></param>
        private void ChangeCarStatusToEnterOnCameraArea(Camera camera, CarAccessInfo carAccessInfo)
        {
            carAccessInfo.Car.State = _db.CarStates.First(x => x.Name == "На въезде" && x.Area == camera.Area);
            _db.SaveChangesAsync();
        }
    }
}
