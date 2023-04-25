using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class BeforeEnterRole : CameraRoleBase
    {
        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService) : base(logger, waitingListsService)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";

            using(var db = new WarehouseContext())
            {
                ExpectedStates = db.CarStates.Where(x => x.Name == "Ожидается").ToList();
            }
        }

        protected override void OnCarWithTempAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithTempAccess(camera, car, list);

            if (car?.CarState?.Area == camera.Area)
            {
                Logger.Info($"{camera.Name}: Для машины ({car.PlateNumberForward}) открыть шлагбаум и сменить статус на \"На въезде\"");
                OpenBarrier(camera);
                ChangeStatus(camera, car, (db, camera, car)=> db.CarStates.First(x => x.Name == "На въезде" && x.Area == camera.Area));
            }
            else
            {
                Logger.Warn($"{camera.Name}: Машина ({car?.PlateNumberForward}) имела неожиданный статус. Ожидаемый статус: \"Ожидается на {camera?.Area?.Name}\". Текущий статус: \"{car?.CarState?.Name} на {car?.CarState?.Area?.Name}\". Без действий.");
                return;
            }
        }

        protected override void OnCarWithFreeAccess(Camera camera, Car car, WaitingList list)
        {
            base.OnCarWithFreeAccess(camera, car, list);

            // TODO: Вынести входящий статус и выходящий статус в конфиг роли
            if (car.CarState.Name == "Ожидается" && car.CarState.Area == camera.Area)
            {
                Logger.Info($"{camera.Name}: Для машины ({car.PlateNumberForward}) открыть шлагбаум и сменить статус на \"На въезде\"");
                OpenBarrier(camera);
                ChangeStatus(camera, car, (db, camera, car) => db.CarStates.First(x => x.Name == "На въезде" && x.Area == camera.Area));
            }
            else
            {
                Logger.Warn($"{camera.Name}: Машина ({car.PlateNumberForward}) имела неожиданный статус. Ожидаемый статус: \"Ожидается на {camera.Area.Name}\". Текущий статус: \"{car.CarState.Name}\". Без действий.");
                return;
            }
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

        private void OpenBarrier(Camera camera)
        {
            //TODO: Открыть шлагбаум
        }

    }
}
