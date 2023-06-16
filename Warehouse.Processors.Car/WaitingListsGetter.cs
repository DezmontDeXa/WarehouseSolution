using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class WaitingListsGetter : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public WaitingListsGetter(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            info.WaitingLists = dbMethods.GetCarsWithWaitingLists().First(x => x.Id == info.Car.Id).WaitingLists;
            return ProcessorResult.Next;
        }
    }

}