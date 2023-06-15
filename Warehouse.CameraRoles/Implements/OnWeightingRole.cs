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
    public class OnWeightingRole : CameraRoleBase
    {
        public OnWeightingRole(
            ILogger logger, IWaitingListsService waitingListsService, IBarriersService barriersService,
            IRussificationService ruService, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
            : base(logger, waitingListsService, barriersService, ruService, settings, dbMethods)
        {
            Id = 3;
            Name = "На весовой";
            Description = "Проверка что машина ожидает взвешивание и смена статуса на \"Взвешивание\"";

            AddExpectedState(new AwaitingWeighingState());
            AddExpectedState(new WeighingState());
            AddExpectedState(new OnEnterState());
            AddExpectedState(new ExitPassGrantedState());
        }

        protected override void OnCarWithFreeAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            ProcessCar(camera, car);
        }

        protected override void OnCarWithTempAccess(ICamera camera, ICarAccessInfo info, ICameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, pictureBlock);
            var car = info.Car;
            ProcessCar(camera, car);
        }

        protected override bool IfNotExpectedCarState(ICarStateType carState, List<int> expectedStateIds)
        {
            return true;
        }

        private void ProcessCar(ICamera camera, ICar car)
        {
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new WeighingState().Id);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на весы. Статус машины изменен на \"{new WeighingState().Name}\".");
        }
    }
}
