using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class AfterEnterRole : CameraRoleBase
    {
        private readonly WarehouseContext _db;

        public AfterEnterRole(ILogger logger, WarehouseContext db, WaitingListsService waitingListsService) : base(logger, waitingListsService)
        {
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";
            _db = db;
        }

        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            if (carAccessInfo.Car == null || carAccessInfo.List == null)
            {
                Logger.Warn($"{camera.Name}: Обнаружена незарегистрированная машина ({plateNumber}) или ее нет в списках. Без действий.");
                return;
            }

            if(carAccessInfo.Car.State?.Name != "На въезде" && carAccessInfo.Car.State?.Area != camera.Area)
            {
                Logger.Warn($"{camera.Name}: Машина ({plateNumber}) имела неожиданный статус. Ожидаемый статус: \"На въезде на {camera.Area.Name}\". Текущий статус: \"{carAccessInfo.Car.State.Name}\". Без действий.");
                return;
            }

            if (carAccessInfo.AccessType == AccessGrantType.Free)
            {
                Logger.Warn($"{camera.Name}: Прибыла машина из постоянного списка ({carAccessInfo.List.Name}) с номером ({plateNumber}) и направлением ({direction}). Без действий.");
                return;
            }

            if (carAccessInfo.AccessType == AccessGrantType.Tracked)
            {
                Logger.Info($"{camera.Name}: Прибыла машина из временного списка ({carAccessInfo.List.Name}) с номером ({plateNumber}). Сменяем статус на \"Ожидает первое взвешивание\".");
                ChangeCarStatusToFirstWeightingOnCameraArea(camera, carAccessInfo);
                return;
            }
        }

        /// <summary>
        /// Сменить статус машины на "Ожидает первое взвешивание" в территорию камеры
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="carAccessInfo"></param>
        private void ChangeCarStatusToFirstWeightingOnCameraArea(Camera camera, CarAccessInfo carAccessInfo)
        {
            carAccessInfo.Car.State = _db.CarStates.First(x => x.Name == "Ожидает первое взвешивание" && x.Area == camera.Area);
            _db.SaveChangesAsync();
        }

    }
}
