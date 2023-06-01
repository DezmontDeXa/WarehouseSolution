using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class AfterEnterRole : CameraRoleBase
    {

        public AfterEnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 2;
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new OnEnterState());                
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, _pictureBlock);
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeStatus(camera, car.Id, new AwaitingWeighingState().Id);
            var area = GetCameraArea(camera);                       

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new AwaitingWeighingState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, _pictureBlock);
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeStatus(camera, car.Id, new ExitPassGrantedState().Id);
            var area = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на территорию {area?.Name}. Статус машины изменен на \"{new ExitPassGrantedState().Name}\".");
        }
    }
}
