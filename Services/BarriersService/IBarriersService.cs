using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public interface IBarriersService
    {
        void Open(BarrierInfo barrier);
    }
}