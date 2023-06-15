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
    public class AfterEnterRole : CameraRoleBase
    {
        public AfterEnterRole(
            ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService,
            IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
            : base(logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 2;
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";
            AddExpectedState(new OnEnterState());
        }

        protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new AwaitingWeighingState().Id);
            var area = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new AwaitingWeighingState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new ExitPassGrantedState().Id);
            var area = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new ExitPassGrantedState().Name}\".");
        }

        protected override bool IfNotExpectedCarState(ICarStateType carState, List<int> expectedStateIds)
        {
            return true;
        }
    }
}
