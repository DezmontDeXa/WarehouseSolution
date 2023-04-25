using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        private readonly WarehouseContext _db;

        public BeforeEnterRole(ILogger logger, WarehouseContext db, WaitingListsService waitingListsService) : base(logger, waitingListsService)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";
            this._db = db;
        }


        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            if(carAccessInfo.Car == null || carAccessInfo.List == null)
            {
                ProcessNotTrakedCar(camera, plateNumber, carAccessInfo);
                return;
            }

            switch (carAccessInfo.AccessType)
            {
                case AccessGrantType.Free:
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
            Logger.Warn($"{camera.Name}: Обнаружена неезарегистрированная машина ({plateNumber}) или ее нет в списках. Уведомляем кпп.");
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
                Logger.Warn($"{camera.Name}: Машина ({plateNumber}) имела неожиданный статус. Ожидаемый статус: \"Ожидается на {camera.Area.Name}\". Текущий статус: \"{carAccessInfo.Car.State.Name}\". Без действий.");
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
