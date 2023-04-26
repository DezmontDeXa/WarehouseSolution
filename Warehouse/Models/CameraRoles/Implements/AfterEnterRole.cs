using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class AfterEnterRole : CameraRoleBase
    {
        private CarState _firstWeighingState;
        private CarState _canExitState;
        private int _targetAreaId = 1;

        public AfterEnterRole(ILogger logger, WaitingListsService waitingListsService) : base(logger, waitingListsService)
        {
            Name = "После въезда";
            Description = "Подтверждение въезда машины на территорию";

            //using(var db = new WarehouseContext())
            //{
            //    ExpectedStates = db.CarStates.Where(x => x.Name == "На въезде").ToList();
            //    _firstWeighingState = db.CarStates.First(x => x.Name == "Ожидает первое взвешивание");
            //    _canExitState = db.CarStates.First(x => x.Name == "Выезд разрешен");
            //}
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);
            ChangeStatus(camera, car, _firstWeighingState);
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithFreeAccess(camera, car, list);
            ChangeStatus(camera, car, _canExitState);
        }
    }
}
