using NLog;

namespace Warehouse.Models.Commands
{
    public abstract class CommandBase
    {
        protected readonly ILogger _logger;

        public CommandBase(ILogger logger)
        {
            _logger = logger;
        }

        public abstract void Execute(object executer, string plateNumber);
    }
}
