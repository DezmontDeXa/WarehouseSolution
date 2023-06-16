using Warehouse.CameraRoles;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.ExprectedStates
{

    public class Expect
    {
        public Expect(ICameraRoleBase role, ICarStateBase[] expectedStates)
        {
            Role = role;
            ExpectedStates = expectedStates;
        }

        public ICameraRoleBase Role { get; }

        public ICarStateBase[] ExpectedStates { get; }
    }
}