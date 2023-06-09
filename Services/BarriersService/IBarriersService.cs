using SharedLibrary.DataBaseModels;
using WarehouseConfgisService.Models;

namespace Warehouse.Services
{
    public interface IBarriersService
    {
        void Open(BarrierInfo barrier);
    }
}