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

        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService) : base(logger, waitingListsService)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

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

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);

            if (car.CarStateId == new AwaitingState().Id)
            {
                ChangeStatus(camera, car, _onEnterState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);
                return;
            }
            
            if (car.CarStateId == new ChangingAreaState().Id)
            {
                ChangeStatus(camera, car, _loagingState);
                SetCarArea(camera, car, camera.Area);
                OpenBarrier(camera, car);
                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithFreeAccess(camera, car, list);

            ChangeStatus(camera, car, _onEnterState);
            SetCarArea(camera, car, camera.Area);
            OpenBarrier(camera, car);
        }

        protected override void OnCarNotFound(Camera camera, CameraNotifyBlock notifyBlock, string plateNumber, string direction)
        {
            base.OnCarNotFound(camera, notifyBlock, plateNumber, direction);
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }

        protected override void OnCarNotInLists(Camera camera, CameraNotifyBlock notifyBlock, Car car, string plateNumber, string direction)
        {
            base.OnCarNotInLists(camera, notifyBlock, car, plateNumber, direction);
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
        }
    }
}
