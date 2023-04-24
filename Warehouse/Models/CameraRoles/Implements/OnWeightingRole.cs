using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        public OnWeightingRole(ILogger logger) : base(logger)
        {
            Name = "На весовой";
            Description = "Машина взвешивается";
        }

        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock)
        {

        }
    }
}
