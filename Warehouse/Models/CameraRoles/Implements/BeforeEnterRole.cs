using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 1;
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума с подтверждением заезда";

            AddExpectedState(new AwaitingState());
            AddExpectedState(new ChangingAreaState());
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            var cameraArea = GetCameraArea(camera);

            if (car.CarStateId == new AwaitingState().Id)
            {
                PassCar(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
                return;
            }

            if (car.CarStateId == new ChangingAreaState().Id)
            {
                var targetArea = GetCarTargetArea(car);

                if (car.TargetAreaId != camera.AreaId)
                {
                    SetErrorStatus(camera, car.Id);

                    using (var db = new WarehouseContext())
                    {
                        Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {targetArea?.Name}, но подъехала к {cameraArea?.Name}. Статус машины изменен на \"{new ErrorState().Name}\".");
                        return;
                    }
                }

                PassCar(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) вернулась на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
                return;
            }
        }

        private void PassCar(Camera camera, Car car)
        {
            ChangeStatus(camera, car.Id, new OnEnterState().Id);
            SetCarArea(camera, car.Id, camera.AreaId);
            OpenBarrier(camera, car);
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);

            ChangeStatus(camera, car.Id, new OnEnterState().Id);
            SetCarArea(camera, car.Id, camera.AreaId);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
        }

        protected override void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            base.OnCarNotFound(camera, notifyBlock, pictureBlock, plateNumber, direction);
            SendUnknownCarNotify(camera, pictureBlock, plateNumber, direction);
        }

        protected override void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, Car car, string plateNumber, string direction)
        {
            base.OnCarNotInLists(camera, notifyBlock, pictureBlock, car, plateNumber, direction);
            SendNotInListCarNotify(camera, car, pictureBlock, plateNumber, direction);
        }
    }
}
