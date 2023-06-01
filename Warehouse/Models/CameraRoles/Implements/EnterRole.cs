using CameraListenerService;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class EnterRole : CameraRoleBase
    {
        public EnterRole(ILogger logger, WaitingListsService waitingListsService, IBarriersService barriersService) : base(logger, waitingListsService, barriersService)
        {
            Id = 5;
            Name = "На въезде";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

            using (var db = new WarehouseContext())
            {
                AddExpectedState(new ChangingAreaState());
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithTempAccess(camera, car, list, pictureBlock);
            var cameraArea = GetCameraArea(camera);

            if (car.CarStateId == new ChangingAreaState().Id)
            {
                if (car.TargetAreaId != camera.AreaId)
                {
                    SetErrorStatus(camera, car.Id);
                    var targetArea = GetCarTargetArea(car);
                    Logger.Warn($"{camera.Name}:\t Машина ({car.PlateNumberForward}) ожидалась на {targetArea?.Name}, но подъехала к {cameraArea?.Name}. Статус машины изменен на \"{new ErrorState().Name}\".");
                    return;
                }

                ChangeStatus(camera, car.Id, new LoadingState().Id);
                SetCarArea(camera, car.Id, cameraArea.Id);
                OpenBarrier(camera, car);

                Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name}. Статус машины изменен на \"{new LoadingState().Name}\".");

                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list, CameraNotifyBlock pictureBlock)
        {
            base.OnCarWithFreeAccess(camera, car, list, pictureBlock);

            ChangeStatus(camera, car.Id, new LoadingState().Id);
            SetCarArea(camera, car.Id, camera.AreaId);
            OpenBarrier(camera, car);

            var cameraArea = GetCameraArea(camera);

            Logger.Info($"{camera.Name}:\t Машина ({car.PlateNumberForward}) прибыла на {cameraArea.Name} по постоянному списку. Статус машины изменен на \"{new LoadingState().Name}\".");
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
