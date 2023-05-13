using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    public class DummyBarrierService : IBarriersService
    {
        public void Switch(BarrierInfo barrier, SimpleBarrierService.BarrierCommand command)
        {

        }
    }
}