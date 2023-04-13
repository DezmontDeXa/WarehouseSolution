using Microsoft.Extensions.Logging;
using Warehouse.Data;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger logger;
        private readonly WarehouseDataBase db;

        public WarehouseSystem(ILogger logger, WarehouseDataBase db)
        {
            this.logger = logger;
            this.db = db;
        }

        public void Run()
        {
            ConfigureServices();
        }

        private void ConfigureServices()
        {

        }
    }
}
