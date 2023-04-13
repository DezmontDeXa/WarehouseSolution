using Microsoft.Extensions.Logging;
using Warehouse.CameraRoles;
using Warehouse.Models;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly WarehouseContext _db;
        private readonly List<CameraRoleBase> _cameraRoles;

        public WarehouseSystem(ILogger logger, WarehouseContext db)
        {
            _logger = logger;
            _db = db;
        }


        public void Run()
        {

        }
    }
}
