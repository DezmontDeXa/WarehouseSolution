using Warehouse.DBModels;

namespace Warehouse.CameraRoles
{
    public class CameraRolesToDB
    {
        private readonly WarehouseContext db;
        private readonly List<CameraRoleBase> cameraRoles;

        public CameraRolesToDB(WarehouseContext db, List<CameraRoleBase> cameraRoles)
        {
            this.db = db;
            this.cameraRoles = cameraRoles;
        }

        public void AddExistingCameraRolesToDB()
        {
            foreach (var role in cameraRoles)
                role.Execute(db);

            db.SaveChanges();
        }
    }
}
