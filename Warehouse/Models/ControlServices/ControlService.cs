using Warehouse.Data;

namespace Warehouse.Models.ControlServices
{
    public abstract class ControlService
    {
        protected WarehouseDataBase DB { get; set; }
    }


}
