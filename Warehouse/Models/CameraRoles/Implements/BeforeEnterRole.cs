using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        private CarState _onEnterState;
        private CarState _loagingState;

        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума с подтверждением заезда";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    GetDbCarStateByType<AwaitingState>(db),
                    GetDbCarStateByType<ChangingAreaState>(db),
                };

                _onEnterState = GetDbCarStateByType<OnEnterState>(db);
                _loagingState = GetDbCarStateByType<LoadingState>(db);
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            var cameraArea = GetCameraArea(camera);

            if (car.CarStateId == new AwaitingState().Id)
            {
                PassCar(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{_onEnterState.Name}\".");
                return;
            }

            if (car.CarStateId == new ChangingAreaState().Id)
            {
                var targetArea = GetCarTargetArea(car);

                if (car.TargetAreaId != camera.AreaId)
                {
                    var state = SetErrorStatus(camera, car);

                    using (var db = new WarehouseContext())
                    {
                        Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {targetArea.Name}, но подъехала к {cameraArea.Name}. Статус машины изменен на \"{state.Name}\".");
                        return;
                    }                    
                }

                PassCar(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) вернулась на {cameraArea.Name}. Статус машины изменен на \"{_onEnterState.Name}\".");
                return;
            }
        }

        private void PassCar(Camera camera, Car car)
        {
            ChangeStatus(camera, car, _onEnterState);
            SetCarArea(camera, car, camera.AreaId);
            OpenBarrier(camera, car);
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);

            ChangeStatus(camera, car, _onEnterState);
            SetCarArea(camera, car, camera.AreaId);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{_onEnterState.Name}\".");
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
