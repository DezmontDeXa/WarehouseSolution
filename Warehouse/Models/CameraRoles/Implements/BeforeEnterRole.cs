using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        private CarState _onEnterState;
        private CarState _loagingState;

        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума с подтверждением заезда";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    GetDbCarStateByType<AwaitingState>(db),
                    GetDbCarStateByType<ChangingAreaState>(db),
                };

                _onEnterState = GetDbCarStateByType<OnEnterState>(db);
                _loagingState = GetDbCarStateByType<LoadingState>(db);
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            if (car.CarStateId == new AwaitingState().Id)
            {
                ChangeStatus(camera, car, _onEnterState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {camera.Area.Name}. Статус машины изменен на \"{_onEnterState.Name}\".");

                return;
            }
            
            if (car.CarStateId == new ChangingAreaState().Id)
            {
                if (car.TargetAreaId != camera.AreaId)
                {
                    SetErrorStatus(camera, car);
                    Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {car.TargetArea.Name}, но подъехала к {camera.Area.Name}. Статус машины изменен на \"Ошибка\".");
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

            ChangeStatus(camera, car, _onEnterState);
            SetCarArea(camera, car, camera.Area);
            OpenBarrier(camera, car);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {camera.Area.Name}. Статус машины изменен на \"{_onEnterState.Name}\".");
        }

        protected override void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, string plateNumber, string direction)
        {
            base.OnCarNotFound(camera, notifyBlock, pictureBlock, plateNumber, direction);
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }

        protected override void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, CameraNotifyBlock pictureBlock, Car car, string plateNumber, string direction)
        {
            base.OnCarNotInLists(camera, notifyBlock, pictureBlock, car, plateNumber, direction);
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }
    }
}
