using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CheckStatusCameraRole
    {
        private readonly WarehouseContext _db;

        public OnWeightingRole(ILogger logger, WarehouseContext db, WaitingListsService waitingList) : base(logger, db, waitingList, db.CarStates.First(x=>x.Id == 7))
        {
            Name = "На весовой";
            Description = "Проверка что машина ождает взвешивание и смена статуса на \"Взвешивание\"";
            _db = db;
        }

        protected override void ProcessFreeCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            Logger.Warn($"{camera.Name}: Прибыла машина из постоянного списка ({carAccessInfo.List.Name}) с номером ({plateNumber}) и направлением ({direction}). Без действий.");
        }

        protected override void ProcessTrackedCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            Logger.Info($"{camera.Name}: Прибыла машина из временного списка ({carAccessInfo.List.Name}) с номером ({plateNumber}). Сменяем статус на \"Первое взвешивание\".");
            ChangeCarStatusToFirstWeightingOnCameraArea(camera, carAccessInfo);
        }

        /// <summary>
        /// Сменить статус машины на "Ожидает первое взвешивание" в территорию камеры
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="carAccessInfo"></param>
        private void ChangeCarStatusToFirstWeightingOnCameraArea(Camera camera, CarAccessInfo carAccessInfo)
        {
            carAccessInfo.Car.CarState = _db.CarStates.First(x => x.Name == "Первое взвешивание" && x.Area == camera.Area);
            _db.SaveChangesAsync();
        }
    }
}
