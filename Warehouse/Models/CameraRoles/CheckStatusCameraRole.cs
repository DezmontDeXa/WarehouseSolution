using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CheckStatusCameraRole : CameraRoleBase
    {
        private readonly WarehouseContext _db;

        private CarState _requiredCarState;

        public CheckStatusCameraRole(ILogger logger, WarehouseContext db, WaitingListsService waitingList, CarState requredCarState) : base(logger, waitingList)
        {
            _db = db;
            _requiredCarState = requredCarState;
        }

        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            if (_requiredCarState == null)
            {
                Logger.Error($"{GetType().Name}({nameof(CheckStatusCameraRole)}): {nameof(_requiredCarState)} must be defined before first OnExecute invoke. No actions.");
                return;
            }

            if (carAccessInfo.Car == null || carAccessInfo.List == null)
            {
                Logger.Warn($"{camera.Name}: Обнаружена незарегистрированная машина ({plateNumber}) или ее нет в списках. Без действий.");
                return;
            }

            if (carAccessInfo.Car.State != _requiredCarState || carAccessInfo.Car.State?.Area != camera.Area)
            {
                Logger.Warn($"{camera.Name}: Машина ({plateNumber}) имела неожиданный статус. Ожидаемый статус: \"{_requiredCarState.Name} на {_requiredCarState.Area.Name}\". Текущий статус: \"{carAccessInfo.Car.State.Name} на {camera.Area.Name}\". Без действий.");
                return;
            }


            if (carAccessInfo.AccessType == AccessGrantType.Free)
            {
                ProcessFreeCar(camera, notifyBlock, carAccessInfo, plateNumber, direction);
                return;
            }

            if (carAccessInfo.AccessType == AccessGrantType.Tracked)
            {
                ProcessTrackedCar(camera, notifyBlock, carAccessInfo, plateNumber, direction);
                return;
            }
        }

        protected abstract void ProcessFreeCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction);

        protected abstract void ProcessTrackedCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction);
    }
}
