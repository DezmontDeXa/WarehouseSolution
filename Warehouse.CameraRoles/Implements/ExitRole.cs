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
    public class ExitRole : CameraRoleBase
    {
        public ExitRole()
            //ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService,
            //IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
            //: base(logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 4;
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            //AddExpectedState(new ExitPassGrantedState());
            //AddExpectedState(new ExitingForChangeAreaState());
        }

        //protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
        //{
        //    base.OnCarWithFreeAccess(camera, info, pictureBlock);
        //    var car = info.Car;
        //    ProcessCar(camera, car);
        //}

        //protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
        //{
        //    base.OnCarWithTempAccess(camera, info, pictureBlock);
        //    var car = info.Car;
        //    ProcessCar(camera, car);
        //}

        //protected override bool IfNotExpectedCarState(ICarStateType carState, List<int> expectedStateIds)
        //{
        //    return true;
        //}

        //private void ProcessCar(ICamera camera, ICar car)
        //{
        //    var cameraArea = GetCameraArea(camera);
        //    var targetArea = GetCarTargetArea(car);

        //    if (car.IsInspectionRequired)
        //    {
        //        SendInspectionRequiredCarNotify(car);

        //        Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) требует провести досмотр. Уведомляем КПП.");
        //        return;
        //    }

        //    // Для выезда с целью смены территории
        //    if (car.CarStateId == new ExitingForChangeAreaState().Id)
        //    {
        //        ChangeCarStatus(camera, car.Id, new ChangingAreaState().Id);
        //        SetCarArea(camera, car.Id, null);
        //        OpenBarrier(camera, car);

        //        Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {targetArea.Name}. Статус машины изменен на \"{new ChangingAreaState().Name}\".");
        //        return;
        //    }

        //    // Для выезда с концами
        //    if (car.CarStateId == new ExitPassGrantedState().Id)
        //    {
        //        ChangeCarStatus(camera, car.Id, new FinishState().Id);
        //        SetCarArea(camera, car.Id, null);
        //        OpenBarrier(camera, car);

        //        Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) уезжает. Статус машины изменен на \"{new FinishState().Name}\".");

        //        return;
        //    }


        //    ChangeCarStatus(camera, car.Id, new FinishState().Id);
        //    SetCarArea(camera, car.Id, null);
        //    OpenBarrier(camera, car);
        //    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) уезжает. Статус машины изменен на \"{new FinishState().Name}\".");
        //}
    }
}
