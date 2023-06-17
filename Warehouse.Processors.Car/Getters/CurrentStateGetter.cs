using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class CurrentStateGetter : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbmethods;

        public CurrentStateGetter(IWarehouseDataBaseMethods dbmethods, ILogger logger) : base(logger)
        {
            this.dbmethods = dbmethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (info.Car is null)
                throw new ArgumentNullException(nameof(info.Car));

            info.State = dbmethods.GetCarState(info.Car);
            return ProcessorResult.Next;
        }
    }

}