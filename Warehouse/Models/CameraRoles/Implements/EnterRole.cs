using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using WaitingListsService;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;
using WarehouseConfgisService.Models;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class EnterRole : CameraRoleBase
    {
        public EnterRole(ILogger logger, WaitingLists waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 5;
            Name = "На въезде";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new ChangingAreaState());
            }
        }


        protected override void OnCarWithTempAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, pictureBlock);
            var cameraArea = GetCameraArea(camera);
            var car = info.Car;
            var targetState = GetTargetCarState(info);

            if (car.CarStateId == new AwaitingState().Id)
            {
                if (InvalideWaitingCamera(camera, info, car, cameraArea))
                    return;

                PassCar(camera, cameraArea, car, targetState);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{targetState.Name}\".");
            }

            if (car.CarStateId == new ChangingAreaState().Id)
            {
                if (car.TargetAreaId != camera.AreaId)
                {
                    SetCarErrorStatus(camera, car.Id);
                    var targetArea = GetCarTargetArea(car);
                    Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {targetArea?.Name}, но подъехала к {cameraArea?.Name}. Статус машины изменен на \"{new ErrorState().Name}\".");
                    return;
                }

                PassCar(camera, cameraArea, car, targetState);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{targetState.Name}\".");
                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, pictureBlock);

            var car = info.Car;
            var cameraArea = GetCameraArea(camera);
            var targetState = GetTargetCarState(info);

            if (info.TopAccessTypeList?.Camera != null)
                if (InvalideWaitingCamera(camera, info, car, cameraArea))
                    return;

            PassCar(camera, cameraArea, car, targetState);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name} по постоянному списку. Статус машины изменен на \"{targetState.Name}\".");
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
        

        private void PassCar(Camera camera, Area? cameraArea, Car car, CarStateBase targetState)
        {
            ChangeCarStatus(camera, car.Id, targetState.Id);
            SetCarArea(camera, car.Id, cameraArea?.Id);
            OpenBarrier(camera, car);
        }
        private static CarStateBase GetTargetCarState(CarAccessInfo info)
        {
            CarStateBase targetState = new ErrorState();
            switch (info.TopPurposeOfArrival)
            {
                case PurposeOfArrival.Unloading:
                    targetState = new UnloadingState();
                    break;
                case PurposeOfArrival.Loading:
                    targetState = new LoadingState();
                    break;
                case PurposeOfArrival.Sampling:
                    targetState = new SamplingState();
                    break;
                case PurposeOfArrival.None:
                    targetState = new ExitPassGrantedState();
                    break;
            }

            return targetState;
        }
    }
}
