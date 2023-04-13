using NLog;
using Warehouse.Data;

namespace Warehouse.Models.Commands
{
    public class ChangeStatusCommand : CommandBase
    {
        private readonly CarStatus _toStatus;

        private readonly WarehouseDataBase _db;

        public ChangeStatusCommand(ILogger logger, WarehouseDataBase db, CarStatus toStatus) : base(logger)
        {
            _db = db;
            _toStatus = toStatus;
        }

        public override void Execute(object executer, string plateNumber)
        {
            ChangeStatus(executer, plateNumber);
        }

        private void ChangeStatus(object executer, string plateNumber)
        {
            //TODO: Change status
            _logger.Trace($"Сменили статус для {plateNumber} на {_toStatus} по команде от {executer}");
        }
    }
}
