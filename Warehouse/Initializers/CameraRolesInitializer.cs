using Warehouse.CameraRoles;
using Warehouse.Models;

namespace Warehouse.Initializers
{
    public class CameraRolesInitializer
    {
        private readonly WarehouseContext db;
        private readonly List<CameraRoleBase> cameraRoles;

        public CameraRolesInitializer(WarehouseContext db, List<CameraRoleBase> cameraRoles)
        {
            this.db = db;
            this.cameraRoles = cameraRoles;
        }

        public void Initialize()
        {
            foreach (var role in cameraRoles)
                role.Initialize(db);
        }
    }
}
