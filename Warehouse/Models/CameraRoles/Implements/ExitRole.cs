using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        private CarState _changingAreaState;
        private CarState _finishState;

        public ExitRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    db.CarStates.ToList().First(x=>CarStateBase.Equals<ExitPassGrantedState>(x)),
                    db.CarStates.ToList().First(x=>CarStateBase.Equals<ExitingForChangeAreaState>(x)),
                };
                _changingAreaState = db.CarStates.ToList().First(x => CarStateBase.Equals<ChangingAreaState>(x));
                _finishState = db.CarStates.ToList().First(x => CarStateBase.Equals<FinishState>(x));
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithFreeAccess(camera, car, list);
            ChangeStatus(camera, car, (db, camera, car) => db.CarStates.First(x => x.Name == "Ожидается"));
            SetCarArea(car, camera.Area);
            OpenBarrier(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);

            if (car.CarStateContext.StartsWith("TargetAreaId="))
            {
                //var targetAreaId = int.Parse(car.CarStateContext.Split('=').Last());
                ChangeStatus(camera, car, _changingAreaState);
            }
            else
            {
                ChangeStatus(camera, car, _finishState);
            }
        }
    }
}
