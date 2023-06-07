using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        public ExitRole(ILogger logger, WaitingListsService waitingList, IBarriersService barriersService) : base(logger, waitingList, barriersService)
        {
            Id = 4;
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new ExitPassGrantedState());
                AddExpectedState(new ExitingForChangeAreaState());
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);
            ProcessCar(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);
            ProcessCar(camera, car);
        }

        private void ProcessCar(Camera camera, Car car)
        {
            var cameraArea = GetCameraArea(camera);
            var targetArea = GetCarTargetArea(car);

            if (car.IsInspectionRequired)
            {
                SendInspectionRequiredCarNotify(car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) требует провести досмотр. Уведомляем КПП.");
                return;
            }

            // Для выезда с целью смены территории
            if (car.CarStateId == new ExitingForChangeAreaState().Id)
            {
                ChangeStatus(camera, car.Id, new ChangingAreaState().Id);
                SetCarArea(camera, car.Id, null);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {targetArea.Name}. Статус машины изменен на \"{new ChangingAreaState().Name}\".");
                return;
            }

            // Для выезда с концами
            if (car.CarStateId == new ExitPassGrantedState().Id)
            {
                ChangeStatus(camera, car.Id, new FinishState().Id);
                SetCarArea(camera, car.Id, null);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) уезжает. Статус машины изменен на \"{new FinishState().Name}\".");

                return;
            }
        }
    }
}
