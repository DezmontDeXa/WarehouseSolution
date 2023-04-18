using NLog;
using Warehouse.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles
{
    public class BeforeEnterRole : CameraRoleBase
    {
        private readonly WaitingListsService _waitingListsService;

        public BeforeEnterRole(ILogger logger, WaitingListsService waitingListsService) : base(logger)
        {
            Name = "Перед въездом";
            Description = "Обнаружение машины перед шлагбаумом и открытие шлагбаума";
            _waitingListsService = waitingListsService;
        }

        protected override void OnExecute(Camera camera)
        {
            // TODO: Проверить номер по спискам (_waitingListsService)
            // TODO: Открыть шлагбаум, если машина в списках. 
            // TODO: Если пустили машину - сменить статус - "На въезде"
            // TODO: Уведомить охрану, если машины нет в списках
        }
    }
}
