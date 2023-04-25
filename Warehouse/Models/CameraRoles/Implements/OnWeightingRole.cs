using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Services;

namespace Warehouse.Models.CameraRoles.Implements
{
    public class OnWeightingRole : CameraRoleBase
    {
        public OnWeightingRole(ILogger logger, WaitingListsService waitingList) : base(logger, waitingList)
        {
            Name = "На весовой";
            Description = "Машина взвешивается";
        }


        protected override void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction)
        {
            throw new NotImplementedException();
        }
    }
}
