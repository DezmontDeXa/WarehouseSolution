using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CameraRoleBase
    {
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

        public void Execute(Camera camera, CameraNotifyBlock notifyBlock)
        {
            try
            {
                // Skip all except Car detected
                if (notifyBlock.EventType != "ANPR")
                    return;

                OnExecute(camera, notifyBlock);
            }
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {RoleName}. Ex: {ex}");
                return;
            }
        }

        protected string GetPlateNumber(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        protected string GetDirection(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
            //return block.XmlDocument.SelectSingleNode("//EventNotificationAlert/ANPR/direction")?.InnerText;
        }

        protected abstract void OnExecute(Camera camera, CameraNotifyBlock notifyBlock);
    }
}
