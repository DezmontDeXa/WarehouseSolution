using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        public BeforeEnterRole()
            //ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService,
            //IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
            //: base(logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 1;
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума с подтверждением заезда";

            //AddExpectedState(new AwaitingState());
            //AddExpectedState(new ChangingAreaState());
        }

        //protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
        //{
        //    base.OnCarWithTempAccess(camera, info, pictureBlock);
        //    var cameraArea = GetCameraArea(camera);
        //    var car = info.Car;

        //    if (car.CarStateId == new AwaitingState().Id)
        //    {
        //        ProcessTempAccessWithAwaitingState(camera, info, cameraArea, car);
        //        return;
        //    }

        //    if (car.CarStateId == new ChangingAreaState().Id)
        //    {
        //        ProcessTempAccessWithChangingAreaState(camera, cameraArea, car);
        //        return;
        //    }

        //    ProcessTempWithAnotherState(camera, car , cameraArea);
        //}

        //protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
        //{
        //    base.OnCarWithFreeAccess(camera, info, pictureBlock);
        //    var car = info.Car;
        //    var cameraArea = GetCameraArea(camera);

        //    //if (info.TopAccessTypeList?.Camera != null)
        //    //    if (InvalideWaitingCamera(camera, info, car, cameraArea))
        //    //        return;

        //    PassCar(camera, car);
        //    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
        //}

        //protected override void OnCarNotFound(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock pictureBlock, string plateNumber, string direction)
        //{
        //    base.OnCarNotFound(camera, notifyBlock, pictureBlock, plateNumber, direction);
        //    SendUnknownCarNotify(camera, pictureBlock, plateNumber, direction);
        //}

        //protected override void OnCarNotInLists(ICamera camera, ICameraNotifyBlock notifyBlock, ICameraNotifyBlock pictureBlock, ICar car, string plateNumber, string direction)
        //{
        //    base.OnCarNotInLists(camera, notifyBlock, pictureBlock, car, plateNumber, direction);
        //    SendNotInListCarNotify(camera, car, pictureBlock, plateNumber, direction);
        //}

        //protected override bool IfNotExpectedCarState(ICarStateType carState, List<int> expectedStateIds)
        //{
        //    return true;
        //}

        //private void ProcessTempAccessWithChangingAreaState(ICamera camera, IArea? cameraArea, ICar car)
        //{
        //    var targetArea = GetCarTargetArea(car);

        //    if (car.TargetAreaId != camera.AreaId)
        //    {
        //        SetCarErrorStatus(camera, car.Id);
        //        Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {targetArea?.Name}, но подъехала к {cameraArea?.Name}. Статус машины изменен на \"{new ErrorState().Name}\".");      
        //    }

        //    PassCar(camera, car);
        //    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) вернулась на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
            
        //}

        //private void ProcessTempAccessWithAwaitingState(ICamera camera, ICarAccessInfo info, IArea? cameraArea, ICar car)
        //{
        //    InvalideWaitingCamera(camera, info, car, cameraArea);

        //    PassCar(camera, car);
        //    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name} с целью {info.TopPurposeOfArrival}. Статус машины изменен на \"{new OnEnterState().Name}\".");
        //}

        //private void ProcessTempWithAnotherState(ICamera camera, ICar car, IArea cameraArea)
        //{
        //    PassCar(camera, car);
        //    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{new OnEnterState().Name}\".");
        //    return;
        //}

        //private void PassCar(ICamera camera, ICar car)
        //{
        //    ChangeCarStatus(camera, car.Id, new OnEnterState().Id);
        //    SetCarArea(camera, car.Id, camera.AreaId);
        //    OpenBarrier(camera, car);
        //}
    }
}
