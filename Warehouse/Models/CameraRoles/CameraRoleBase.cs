using NLog;
using Warehouse.DataBaseModels;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CameraRoleBase
    {
        public int ID { get; set; }
        public string RoleName { get; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected ILogger Logger { get; private set; }

        public CameraRoleBase(ILogger logger)
        {
            RoleName = GetType().Name;
            Logger = logger;
        }

        public void AddThatRoleToDB(WarehouseContext db)
        {
            if (!db.CameraRoles.Any(x => x.TypeName == RoleName))
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = RoleName });
        }

        public void Execute(Camera camera)
        {
            try
            {
                OnExecute(camera);
            }
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {RoleName}. Ex: {ex}");
                return;
            }
        }

        protected abstract void OnExecute(Camera camera);
    }
}
