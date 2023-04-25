using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        public ExitRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "Выезд";
            Description = "Обнаружение машин на выезд и открытие шлагбаума";
        }

        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            if (carAccessInfo.Car == null || carAccessInfo.List == null)
            {
                ProcessNotTrakedCar(camera, plateNumber, carAccessInfo);
                return;
            }

            switch (carAccessInfo.AccessType)
            {
                case AccessGrantType.Free:
                    ProcessFreeCar(camera, notifyBlock, carAccessInfo, plateNumber, direction);
                    break;
                case AccessGrantType.Tracked:
                    ProcessTrackedCar(camera, notifyBlock, carAccessInfo, plateNumber, direction);
                    break;
            }
        }

        private void ProcessNotTrakedCar(Camera camera, string plateNumber, CarAccessInfo carAccessInfo)
        {
            //TODO: Отправить распознаный номер в специальную таблицу БД для дальнейшей обработки охранником.
            Logger.Warn($"{camera.Name}: Обнаружена неезарегистрированная машина ({plateNumber}) или ее нет в списках. Уведомляем кпп.");
        }

        private void ProcessFreeCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            Logger.Info($"{camera.Name}: Выезжает машина из списка {carAccessInfo.List.Name} с номером ({plateNumber}). Открыть шлагбаум. Сменить статус на \"Ожидается\"");
            OpenBarrier(camera, carAccessInfo);
        }

        private void ProcessTrackedCar(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            if (carAccessInfo.Car.CarState == null)
            {
                Logger.Warn($"{camera.Name}: Машина {plateNumber} не имеет статуса. Без действий.");
                return;
            }

        }


        private void OpenBarrier(Camera camera, CarAccessInfo carAccessInfo)
        {
            //TODO: Открыть шлагбаум
        }
    }
}
