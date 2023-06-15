using Warehouse.Interfaces.DataBase;

namespace Warehouse.Processors.Car
{
    public class ChangeCarAreaProcessor : DbProcessorBase
    {
        public ChangeCarAreaProcessor(IWarehouseDataBaseMethods dbmethods) : base(dbmethods)
        {
        }

        public override ProcessorResult Process(CarInfo info)
        {
            DbMethods.SetCarArea(info.Car, info.Camera.AreaId);
            return ProcessorResult.Next;
        }
    }
}