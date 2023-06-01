using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CameraRoles
{
    public class CameraRolesToDB
    {
        private readonly List<CameraRoleBase> cameraRoles;

        public CameraRolesToDB(List<CameraRoleBase> cameraRoles)
        {
            this.cameraRoles = cameraRoles;
        }

        public void AddExistingCameraRolesToDB()
        {
            using (var db = new WarehouseContext())
            {
                foreach (var role in cameraRoles)
                    role.AddThatRoleToDB(db);
                
                db.SaveChanges();
            }
        }
    }
}
