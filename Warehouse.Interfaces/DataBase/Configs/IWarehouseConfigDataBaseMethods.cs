using Warehouse.Interfaces.Barriers;

namespace Warehouse.Interfaces.DataBase.Configs
{
    public interface IWarehouseConfigDataBaseMethods
    {
        IBarrierInfo? GetBarrierInfo(ICamera camera);
    }
}