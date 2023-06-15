using Warehouse.Interfaces.DataBase;

namespace Warehouse.Processors.Car
{
    public class CurrentStateGetter : DbProcessorBase
    {
        public CurrentStateGetter(IWarehouseDataBaseMethods dbmethods) : base(dbmethods)
        {
        }

        public override ProcessorResult Process(CarInfo info)
        {
            info.State =  DbMethods.GetCarState(info.Car);
            return ProcessorResult.Next;
        }
    }
    
}