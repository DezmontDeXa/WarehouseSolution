using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public interface IBarriersService
    {
        void Switch(BarrierInfo barrier, SimpleBarrierService.BarrierCommand command);
    }
}