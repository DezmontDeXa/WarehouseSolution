using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        private CarState _awaitFirstWeighingState;
        private CarState _awaitSecondWeighingState;
        private CarState _firstWeighingState;
        private CarState _secondWeighingState;

        public OnWeightingRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "На весовой";
            Description = "Проверка что машина ожидает взвешивание и смена статуса на \"Взвешивание\"";

            using(var db = new WarehouseContext())
            {
                _awaitFirstWeighingState = db.CarStates.First(x => x.Name == "Ожидает первое взвешивание");
                _awaitSecondWeighingState = db.CarStates.First(x => x.Name == "Ожидает второе взвешивание");
                _firstWeighingState = db.CarStates.First(x => x.Name == "Первое взвешивание");
                _secondWeighingState = db.CarStates.First(x => x.Name == "Второе взвешивание");

                ExpectedStates = new List<CarState>()
                {
                    _awaitFirstWeighingState,
                    _awaitSecondWeighingState,
                    _firstWeighingState,
                    _secondWeighingState
                };
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);

            if (car.CarStateId == _awaitFirstWeighingState.Id)
                ChangeStatus(camera, car, _firstWeighingState);

            if (car.CarStateId == _awaitSecondWeighingState.Id)
                ChangeStatus(camera, car, _secondWeighingState);
        }
    }
}
