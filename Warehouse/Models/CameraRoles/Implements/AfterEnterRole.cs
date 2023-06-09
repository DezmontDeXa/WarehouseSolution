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
    public class AfterEnterRole : CameraRoleBase
    {

        public AfterEnterRole(ILogger logger, WaitingLists waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 2;
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";
            AddExpectedState(new OnEnterState());
        }

        protected override void OnCarWithTempAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new AwaitingWeighingState().Id);
            var area = GetCameraArea(camera);                       

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new AwaitingWeighingState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new ExitPassGrantedState().Id);
            var area = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new ExitPassGrantedState().Name}\".");
        }
    }
}
