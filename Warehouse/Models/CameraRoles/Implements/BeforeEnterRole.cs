using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using WaitingListsService;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;
using WarehouseConfgisService.Models;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        public BeforeEnterRole(ILogger logger, WaitingLists waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 1;
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума с подтверждением заезда";

            AddExpectedState(new AwaitingState());
            AddExpectedState(new ChangingAreaState());
        }

        protected override void OnCarWithTempAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera,info, pictureBlock);

            var cameraArea = GetCameraArea(camera);

            var car = info.Car;


            if (car.CarStateId == new AwaitingState().Id)
            {
                ProcessTempAccessWithAwaitingState(camera, info, cameraArea, car);
                return;
            }

            if (car.CarStateId == new ChangingAreaState().Id)
            {
                ProcessTempAccessWithChangingAreaState(camera, cameraArea, car);
                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, pictureBlock);

            var car = info.Car;
            var cameraArea = GetCameraArea(camera);

            if (info.TopAccessTypeList?.Camera != null)
                if (InvalideWaitingCamera(camera, info, car, cameraArea))
                    return;

            PassCar(camera, car);

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


        private void ProcessTempAccessWithChangingAreaState(Camera camera, Area? cameraArea, Car car)
        {
            var targetArea = GetCarTargetArea(car);

            if (car.TargetAreaId != camera.AreaId)
            {
                SetCarErrorStatus(camera, car.Id);

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

        private void ProcessTempAccessWithAwaitingState(Camera camera, CarAccessInfo info, Area? cameraArea, Car car)
        {
            if (InvalideWaitingCamera(camera, info, car, cameraArea))
                return;

            PassCar(camera, car);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name} с целью {info.TopPurposeOfArrival}. Статус машины изменен на \"{new OnEnterState().Name}\".");
            return;
        }


        private void PassCar(Camera camera, Car car)
        {
            ChangeCarStatus(camera, car.Id, new OnEnterState().Id);
            SetCarArea(camera, car.Id, camera.AreaId);
            OpenBarrier(camera, car);
        }
    }
}
