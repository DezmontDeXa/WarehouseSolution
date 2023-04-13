using NLog;
using Warehouse.Data;
using Warehouse.Models.ControlServices;

namespace Warehouse.Models.Commands
{
    public class OpenBarrierCommand : CommandBase
    {
        private readonly WarehouseDataBase _db;
        private readonly KPPBarrier _barrier;

        public OpenBarrierCommand(ILogger logger, WarehouseDataBase db, KPPBarrier barrier) : base(logger)
        {
            _db = db;
            _barrier = barrier;
        }

        public override void Execute(object executer, string plateNumber)
        {
            OpenBarrier(executer, plateNumber);
        }

        private void OpenBarrier(object executer, string plateNumber)
        {
            _barrier.Open();
            _logger.Trace($"Открыли шлагбаум {_barrier} для {plateNumber} по команде от {executer}");
        }
    }
}
