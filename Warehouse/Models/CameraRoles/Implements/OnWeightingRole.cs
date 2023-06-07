﻿using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        public OnWeightingRole(ILogger logger, WaitingListsService waitingList, IBarriersService barriersService) : base(logger, waitingList, barriersService)
        {
            Id = 3;
            Name = "На весовой";
            Description = "Проверка что машина ожидает взвешивание и смена статуса на \"Взвешивание\"";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new AwaitingWeighingState());
                AddExpectedState(new WeighingState());
                AddExpectedState(new OnEnterState());
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock _pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, _pictureBlock);
            ProcessCar(camera, car);
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);
            ProcessCar(camera, car);
        }

        private void ProcessCar(Camera camera, Car car)
        {
            SetCarArea(camera, car.Id, camera.AreaId);
            ChangeStatus(camera, car.Id, new WeighingState().Id);
            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) заехала на весы. Статус машины изменен на \"{new WeighingState().Name}\".");
        }
    }
}
