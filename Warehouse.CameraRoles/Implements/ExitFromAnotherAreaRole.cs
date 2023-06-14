using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.ConfigDataBase.Context;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.CameraRoles.Implements
{
    public class ExitFromAnotherAreaRole : CameraRoleBase
    {
        private IArea weightControlArea;

        public ExitFromAnotherAreaRole(
            ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService, 
            IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods) 
            :base (logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 6;
            Name = "Выезд с другой территории";
            Description = "Выезд с териитории без весов и открытие шлагбаума";
            AddExpectedState(new LoadingState());
            AddExpectedState(new UnloadingState());
            AddExpectedState(new SamplingState());

            weightControlArea = GetNaisArea();
        }

        protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new ChangingAreaState().Id);
            SetCarTargetArea(camera, car.Id, weightControlArea.Id);
            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {weightControlArea.Name}. Статус машины изменен на \"{new ChangingAreaState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            ChangeCarStatus(camera, car.Id, new AwaitingState().Id);
            SetCarArea(camera, car.Id, null);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) покинула территорию {cameraArea.Name}. Статус машины изменен на \"{new AwaitingState().Name}\".");
        }

        protected override bool IfNotExpectedCarState(ICarState carState, List<int> expectedStateIds)
        {
            return true;
        }
    }
}
