using Warehouse.DBModels;

namespace Warehouse.CameraRoles
{
    public abstract class CameraRoleBase
    {
        public int ID { get; set; }
        public string RoleName { get; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public CameraRoleBase()
        {
            RoleName = GetType().Name;
        }

        public void Execute(WarehouseContext db)
        {
            if (!db.CameraRoles.Any(x => x.TypeName == RoleName))
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = RoleName });
        }
    }
}
