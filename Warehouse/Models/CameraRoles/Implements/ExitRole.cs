using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        private CarState _awaitingState;
        private CarState _changingAreaState;
        private CarState _finishState;

        public ExitRole(ILogger logger, WaitingListsService waitingList, IBarriersService barriersService) : base(logger, waitingList, barriersService)
        {
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    GetDbCarStateByType<ExitPassGrantedState>(db),
                    GetDbCarStateByType<ExitingForChangeAreaState>(db),
                    GetDbCarStateByType<LoadingState>(db)
                };

                _awaitingState = GetDbCarStateByType<AwaitingState>(db);
                _changingAreaState = GetDbCarStateByType<ChangingAreaState>(db);
                _finishState = GetDbCarStateByType<FinishState>(db);
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);
            ChangeStatus(camera, car, _awaitingState);
            SetCarArea(camera, car, null);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) покинула территорию {cameraArea.Name}. Статус машины изменен на \"{_awaitingState.Name}\".");
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            var cameraArea = GetCameraArea(camera);
            var targetArea = GetCarTargetArea(car);

            if (car.IsInspectionRequired)
            {
                SendInspectionRequiredCarNotify(car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) требует провести досмотр. Уведомляем КПП.");
                return;
            }

            // Для выезда с целью смены территории
            if (car.CarStateId == new ExitingForChangeAreaState().Id)
            {
                ChangeStatus(camera, car, _changingAreaState);
                SetCarArea(camera, car, null);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {targetArea.Name}. Статус машины изменен на \"{_changingAreaState.Name}\".");
                return;
            }

            // Для выезда с концами
            if (car.CarStateId == new ExitPassGrantedState().Id)
            {
                ChangeStatus(camera, car, _finishState);
                SetCarArea(camera, car, null);
                OpenBarrier(camera, car);
                
                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) уезжает. Статус машины изменен на \"{_finishState.Name}\".");

                return;
            }

            // Для выезда после разгрузки на другой территории
            if (car.CarStateId == new LoadingState().Id)
            {
                if (car.TargetAreaId == camera.AreaId)
                {
                    ChangeStatus(camera, car, _changingAreaState);
                    SetCarArea(camera, car, null);
                    SetCarTargetArea(camera, car, GetNaisArea());
                    OpenBarrier(camera, car);

                    targetArea = GetCarTargetArea(car);

                    Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) меняет территорию с {cameraArea.Name} на {targetArea.Name}. Статус машины изменен на \"{_changingAreaState.Name}\".");
                }
                return;
            }

        }

        private Area GetNaisArea()
        {
            using(var db = new WarehouseContext())
            {
                var areaId = int.Parse( db.Configs.First(x => x.Key == "NaisAreaId").Value);
                return db.Areas.First(x => x.Id == areaId);
            }
        }
    }
}
