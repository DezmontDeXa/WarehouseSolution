using Ninject;
using NLog;
using Warehouse.Processors.Implements;

namespace Warehouse.Processors.Car.Core
{
    public class WarehousePipeline : ProcessorsPipeline<CarInfo>
    {
        private readonly IKernel kernel;

        public WarehousePipeline(IKernel _kernel)
        {
            var stateSwitchers = new ProcessorsPipeline<CarInfo>();
            stateSwitchers.AddProcessor(_kernel.Get<BlockCarAfterLoadingUnloadingProcessor>());
            stateSwitchers.AddProcessor(_kernel.Get<FreeStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<TrackedStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<AfterFirstWeightningStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<AnotherAreaStateSwitcher>());
            stateSwitchers.AllSkipped += StateSwitchers_AllSkipped;

            AddProcessor(_kernel.Get<PlateNumberGetter>());
            AddProcessor(_kernel.Get<DetectedCarPrinter>());
            AddProcessor(_kernel.Get<DirectionGetter>());
            AddProcessor(_kernel.Get<DirectionFilter>());
            AddProcessor(_kernel.Get<RegisteredCarFilter>());
            AddProcessor(_kernel.Get<ChangeCarAreaProcessor>());
            AddProcessor(_kernel.Get<CurrentStateGetter>());
            AddProcessor(_kernel.Get<StatesFilter>());
            AddProcessor(_kernel.Get<WaitingListsGetter>());
            AddProcessor(_kernel.Get<AccessTypeSelector>());
            AddProcessor(_kernel.Get<PurposesGetter>());
            AddProcessor(stateSwitchers);
            AddProcessor(_kernel.Get<OpenBarrierProcessor>());
            kernel = _kernel;
        }

        private void StateSwitchers_AllSkipped(object? sender, EventArgs e)
        {
            kernel.Get<ILogger>().Error("Статус не был изменен");
        }
    }
}
