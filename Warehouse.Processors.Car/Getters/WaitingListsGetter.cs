using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class WaitingListsGetter : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;
        private readonly IWarehouseConfigDataBaseMethods configMethods;

        public WaitingListsGetter(ILogger logger, IWarehouseDataBaseMethods dbMethods, IWarehouseConfigDataBaseMethods configMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
            this.configMethods = configMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
           var allLists = dbMethods.GetCarsWithWaitingLists().First(x => x.Id == info.Car.Id).WaitingLists;
            var durationHours = configMethods.GetListAliveDuration();
            info.WaitingLists = allLists.Where(x=>x.Date + new TimeSpan(durationHours,0,0) < DateTime.Now).ToList();
            return ProcessorResult.Next;
        }
    }

}