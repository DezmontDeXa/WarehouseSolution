using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using WaitingListsService;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;
using WarehouseConfgisService.Models;
using WarehouseConfigService;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitFromAnotherAreaRole : CameraRoleBase
    {
        Area weightControlArea;

        public ExitFromAnotherAreaRole(ILogger logger, WaitingLists waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 6;
            Name = "Выезд с другой территории";
            Description = "Выезд с териитории без весов и открытие шлагбаума";
            AddExpectedState(new LoadingState());
            AddExpectedState(new UnloadingState());
            AddExpectedState(new SamplingState());

            using (var configsDb = new WarehouseConfig())
            using (var db = new WarehouseContext())
            {
                var value = configsDb.Configs.First(x => x.Key == "NaisAreaId")?.Value;
                var areaId = int.Parse(value);
                weightControlArea = configsDb.Areas.Find(areaId);
            }
        }
         
        protected override void OnCarWithTempAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, _pictureBlock);
            var car = info.Car;
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new ChangingAreaState().Id);
            SetCarTargetArea(camera, car.Id, weightControlArea.Id);
            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {weightControlArea.Name}. Статус машины изменен на \"{new ChangingAreaState().Name}\".");
        }

        protected override void OnCarWithFreeAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            ChangeCarStatus(camera, car.Id, new AwaitingState().Id);
            SetCarArea(camera, car.Id, null);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) покинула территорию {cameraArea.Name}. Статус машины изменен на \"{new AwaitingState().Name}\".");
        }
    }
}
