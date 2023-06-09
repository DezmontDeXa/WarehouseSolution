using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using WaitingListsService;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;
using WarehouseConfgisService.Models;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        public OnWeightingRole(ILogger logger,  WaitingLists waitingList, IBarriersService barriersService) : base(logger, waitingList, barriersService)
        {
            Id = 3;
            Name = "На весовой";
            Description = "Проверка что машина ожидает взвешивание и смена статуса на \"Взвешивание\"";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new AwaitingWeighingState());
                AddExpectedState(new WeighingState());
                AddExpectedState(new OnEnterState());
                AddExpectedState(new ExitPassGrantedState());
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, info, _pictureBlock);
            var car = info.Car;
            ProcessCar(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, CarAccessInfo info, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, info, pictureBlock);
            var car = info.Car;
            ProcessCar(camera, car);
        }

        private void ProcessCar(Camera camera, Car car)
        {
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeCarStatus(camera, car.Id, new WeighingState().Id);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на весы. Статус машины изменен на \"{new WeighingState().Name}\".");
        }
    }
}
