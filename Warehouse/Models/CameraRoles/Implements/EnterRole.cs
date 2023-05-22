using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class EnterRole : CameraRoleBase
    {
        private CarState _loagingState;
        private CarState _changingAreaState;

        public EnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Name = "На въезде";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    GetDbCarStateByType<ChangingAreaState>(db),
                };

                _loagingState = GetDbCarStateByType<LoadingState>(db);
                _changingAreaState = GetDbCarStateByType<ChangingAreaState>(db);
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);
                        
            if (car.CarStateId == _changingAreaState.Id)
            {
                if (car.TargetAreaId != camera.AreaId)
                {
                    var errroState = SetErrorStatus(camera, car);
                    Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {car.TargetArea.Name}, но подъехала к {camera.Area.Name}. Статус машины изменен на \"{errroState.Name}\".");
                    return;
                }

                ChangeStatus(camera, car, _loagingState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {camera.Area.Name}. Статус машины изменен на \"{_loagingState.Name}\".");

                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);

            ChangeStatus(camera, car, _loagingState);
            SetCarArea(camera, car, camera.Area);
            OpenBarrier(camera, car);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {camera.Area.Name} по постоянному списку. Статус машины изменен на \"{_loagingState.Name}\".");
        }

        protected override void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            base.OnCarNotFound(camera, notifyBlock, pictureBlock, plateNumber, direction);
            SendUnknownCarNotify(camera, pictureBlock, plateNumber, direction);
        }

        protected override void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, Car car, string plateNumber, string direction)
        {
            base.OnCarNotInLists(camera, notifyBlock, pictureBlock, car, plateNumber, direction);
            SendNotInListCarNotify(camera, car, pictureBlock, plateNumber, direction);
        }
    }
}
