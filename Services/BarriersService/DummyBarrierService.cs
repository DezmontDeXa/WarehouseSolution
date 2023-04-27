using NLog;

namespace BarriersService
{
    public class DummyBarrierService : IBarriersService
    {
        private readonly ILogger logger;

        public DummyBarrierService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Open(string uri)
        {
            logger.Info($"Открыть шлагбаум по адресу: {uri}");
        }
    }
}