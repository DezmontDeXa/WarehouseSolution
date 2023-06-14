using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.CameraRoles.Implements
{
    public class EnterRole : CameraRoleBase
    {
        public EnterRole(
            ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService,
            IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
            : base(logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 5;
            Name = "На въезде";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

            AddExpectedState(new ChangingAreaState());
        }

        protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
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

        protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
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

        protected override void OnCarNotFound(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            base.OnCarNotFound(camera, notifyBlock, pictureBlock, plateNumber, direction);
            SendUnknownCarNotify(camera, pictureBlock, plateNumber, direction);
        }

        protected override void OnCarNotInLists(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock pictureBlock, ICar car, string plateNumber, string direction)
        {
            base.OnCarNotInLists(camera, notifyBlock, pictureBlock, car, plateNumber, direction);
            SendNotInListCarNotify(camera, car, pictureBlock, plateNumber, direction);
        }

        protected override bool IfNotExpectedCarState(ICarState carState, List<int> expectedStateIds)
        {
            return true;
        }
        private void PassCar(ICamera camera, IArea? cameraArea, ICar car, ICarStateBase targetState)
        {
            ChangeCarStatus(camera, car.Id, targetState.Id);
            SetCarArea(camera, car.Id, cameraArea?.Id);
            OpenBarrier(camera, car);
        }

        private static ICarStateBase GetTargetCarState(ICarAccessInfo info)
        {
            switch (info.TopPurposeOfArrival)
            {
                case PurposeOfArrival.Unloading:
                    return new UnloadingState();
                case PurposeOfArrival.Loading:
                    return  new LoadingState();
                case PurposeOfArrival.Sampling:
                    return  new SamplingState();
                case PurposeOfArrival.None:
                    return  new ExitPassGrantedState();
            }

            return new ErrorState();
        }
    }
}
