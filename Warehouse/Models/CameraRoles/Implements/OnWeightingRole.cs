using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        private CarState _awaitWeighingState;
        private CarState _weighingState;

        public OnWeightingRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "На весовой";
            Description = "Проверка что машина ожидает взвешивание и смена статуса на \"Взвешивание\"";

            using (var db = new WarehouseContext())
            {
                _awaitWeighingState = db.CarStates.ToList().First(x => CarStateBase.Equals<AwaitingWeighingState>(x));
                _weighingState = db.CarStates.ToList().First(x => CarStateBase.Equals<WeighingState>(x));

                ExpectedStates = new List<CarState>()
                {
                    _awaitWeighingState,
                    _weighingState,
                };
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);

            if (car.CarStateId == _awaitWeighingState.Id)
            {
                SetCarArea(camera, car, camera.Area);
                ChangeStatus(camera, car, _weighingState);
            }
        }
    }
}
