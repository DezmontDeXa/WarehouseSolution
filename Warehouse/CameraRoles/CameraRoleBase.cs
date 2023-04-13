using Warehouse.Models;

namespace Warehouse.CameraRoles
{
    public abstract class CameraRoleBase
    {
        public int ID { get; set; }
        public string RoleName { get; }

        public CameraRoleBase()
        {
            RoleName = GetType().Name;
        }

        public void Initialize(WarehouseContext db)
        {
            if (!db.CameraRoles.Any(x => x.Name == RoleName))
            {
                db.CameraRoles.Add(new CameraRole() { Name = RoleName });
                db.SaveChanges();
            }
        }
    }

    public class BeforeEnterRole : CameraRoleBase
    {
    }

    public class AfterEnterRole : CameraRoleBase
    {

    }
}
