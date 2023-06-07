using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitFromAnotherAreaRole : CameraRoleBase
    {
        Area weightControlArea;

        public ExitFromAnotherAreaRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 6;
            Name = "Выезд с другой территории";
            Description = "Выезд с териитории без весов и открытие шлагбаума";
            AddExpectedState(new LoadingState());

            using(var db = new WarehouseContext())
            {
                var value = db.Configs.First(x => x.Key == "NaisAreaId")?.Value;
                var areaId = int.Parse(value);
                weightControlArea = db.Areas.Find(areaId);
            }
        }
         
        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, _pictureBlock);
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeStatus(camera, car.Id, new ChangingAreaState().Id);
            SetCarTargetArea(camera, car.Id, weightControlArea.Id);
            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {weightControlArea.Name}. Статус машины изменен на \"{new ChangingAreaState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, _pictureBlock);
            ChangeStatus(camera, car.Id, new AwaitingState().Id);
            SetCarArea(camera, car.Id, null);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) покинула территорию {cameraArea.Name}. Статус машины изменен на \"{new AwaitingState().Name}\".");
        }
    }
}
