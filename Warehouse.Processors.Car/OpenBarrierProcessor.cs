using NLog;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class OpenBarrierProcessor : CarInfoProcessorBase
    {
        private readonly IBarriersService barriersService;
        private readonly IWarehouseConfigDataBaseMethods configMethods;

        public OpenBarrierProcessor(IBarriersService barriersService, IWarehouseConfigDataBaseMethods configMethods, ILogger logger) : base(logger) 
        {
            this.barriersService = barriersService;
            this.configMethods = configMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var barrier = configMethods.GetBarrierInfo(info.Camera);
            if (barrier == null) 
                return ProcessorResult.Next;

            barriersService.Open(barrier);
            Logger.Info("Шлагбаум открыт");
            return ProcessorResult.Next;
        }
    }
}