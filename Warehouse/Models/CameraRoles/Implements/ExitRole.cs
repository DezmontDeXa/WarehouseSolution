using NLog;
using Warehouse.SharedLibrary;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class ExitRole : CameraRoleBase
    {
        public ExitRole(ILogger logger) : base(logger)
        {
            Name = "Выезд";
            Description = "Машина выезжает с территории";
        }
        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock)
        {

        }
    }
}
