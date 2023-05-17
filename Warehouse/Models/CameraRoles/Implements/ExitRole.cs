using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        private CarState _awaitingState;
        private CarState _changingAreaState;
        private CarState _finishState;

        public ExitRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    GetDbCarStateByType<ExitPassGrantedState>(db),
                    GetDbCarStateByType<ExitingForChangeAreaState>(db),
                    GetDbCarStateByType<LoadingState>(db)
                };

                _awaitingState = GetDbCarStateByType<AwaitingState>(db);
                _changingAreaState = GetDbCarStateByType<ChangingAreaState>(db);
                _finishState = GetDbCarStateByType<FinishState>(db);
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);
            ChangeStatus(camera, car, _awaitingState);
            SetCarArea(camera, car, camera.Area);
            OpenBarrier(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            if (car.IsInspectionRequired)
            {
                Logger.Info($"{camera.Name}: Для машины ({car.PlateNumberForward}) требуется провести досмотр. Уведомляем КПП.");
                // TODO: Уведомить КПП
                return;
            }

            // Для выезда с целью смены территории
            if (car.CarStateId == new ExitingForChangeAreaState().Id)
            {
                ChangeStatus(camera, car, _changingAreaState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);
                return;
            }

            // Для выездас концами
            if (car.CarStateId == new ExitPassGrantedState().Id)
            {
                ChangeStatus(camera, car, _finishState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);
                return;
            }

            // Для выезда с другой территории
            if (car.CarStateId == new LoadingState().Id)
            {
                ChangeStatus(camera, car, _changingAreaState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);
                return;
            }

        }
    }
}
