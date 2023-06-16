using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.Interfaces.CameraRoles
{
    public interface ICameraRoleBase
    {
        string Description { get; }
        int Id { get; }
        string Name { get; }

        //void Execute(ICamera camera, ICameraNotifyBlock notifyBlock);
    }
}