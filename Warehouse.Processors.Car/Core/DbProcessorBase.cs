using Warehouse.Interfaces.DataBase;

namespace Warehouse.Processors.Car.Core
{
    public abstract class DbProcessorBase : IProcessor<CarInfo>
    {
        protected readonly IWarehouseDataBaseMethods DbMethods;

        public DbProcessorBase(IWarehouseDataBaseMethods dbmethods)
        {
            DbMethods = dbmethods;
        }

        public abstract ProcessorResult Process(CarInfo info);
    }
}