using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        private CarState _waitingState;
        private CarState _finishState;

        public ExitRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                ExpectedStates = new List<CarState>()
                {
                    db.CarStates.First(x=>x.Name == "Выезд разрешен"),
                    db.CarStates.First(x=>x.Name == "Выезд на другую территорию разрешен"),                    
                };
                _waitingState = db.CarStates.First(x => x.Name == "Ожидается на другой территории"); 
                _finishState = db.CarStates.First(x => x.Name == "Работа завершена");
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithFreeAccess(camera, car, list);
            ChangeStatus(camera, car, (db, camera, car) => db.CarStates.First(x => x.Name == "Ожидается" && x.Area == camera.Area));
            OpenBarrier(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);

            if(car.CarStateContext.StartsWith("TargetAreaId="))
            {
                //var targetAreaId = int.Parse(car.CarStateContext.Split('=').Last());
                ChangeStatus(camera, car, _waitingState);
            }
            else
            {
                ChangeStatus(camera, car, _finishState);
            }
        }
    }
}
