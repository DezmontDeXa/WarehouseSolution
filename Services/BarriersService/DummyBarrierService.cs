using NLog;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class DummyBarrierService : IBarriersService
    {
        private readonly ILogger logger;

        public DummyBarrierService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Switch(BarrierInfo barrier, SimpleBarrierService.BarrierCommand command)
        {
            logger.Info($"DummyBarrier: {command}");
        }
    }
}