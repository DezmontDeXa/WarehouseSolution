using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class AfterEnterRole : CameraRoleBase
    {
        private CarState _weighingState;
        private CarState _exitPassGrantedState;

        public AfterEnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = db.CarStates.ToList().Where(x => CarStateBase.Equals<OnEnterState>(x)).ToList();
                _weighingState = db.CarStates.ToList().First(x => CarStateBase.Equals<AwaitingWeighingState>(x));
                _exitPassGrantedState = db.CarStates.ToList().First(x => CarStateBase.Equals<ExitPassGrantedState>(x));
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, _pictureBlock);
            SetCarArea(camera, car, camera.Area);
            ChangeStatus(camera, car, _weighingState);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {camera.Area.Name}. Статус машины изменен на \"{_weighingState.Name}\".");
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, _pictureBlock);
            SetCarArea(camera, car, camera.Area);
            ChangeStatus(camera, car, _exitPassGrantedState);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {camera.Area.Name}. Статус машины изменен на \"{_exitPassGrantedState.Name}\".");
        }
    }
}
