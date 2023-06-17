using Ninject;
using NLog;
using Warehouse.Processors.Car.Filters;
using Warehouse.Processors.Car.Getters;
using Warehouse.Processors.Car.Setters;
using Warehouse.Processors.Car.StateSwithers;
using Warehouse.Processors.Implements;

namespace Warehouse.Processors.Car.Core
{
    public class WarehousePipeline : ProcessorsPipeline<CarInfo>
    {
        private readonly IKernel kernel;
        private readonly ILogger logger;

        public WarehousePipeline(IKernel _kernel, ILogger logger)
        {
            kernel = _kernel;
            this.logger = logger;
            var stateSwitchers = new ProcessorsPipeline<CarInfo>();
            stateSwitchers.AddProcessor(_kernel.Get<BlockCarAfterLoadingUnloadingProcessor>());
            stateSwitchers.AddProcessor(_kernel.Get<FreeStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<TrackedStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<AfterFirstWeightningStateSwitcher>());
            stateSwitchers.AddProcessor(_kernel.Get<AnotherAreaStateSwitcher>());
            stateSwitchers.AllSkipped += StateSwitchers_AllSkipped;

            AddProcessor(_kernel.Get<PlateNumberGetter>());
            AddProcessor(_kernel.Get<DirectionGetter>());
            AddProcessor(_kernel.Get<DirectionFilter>());
            AddProcessor(_kernel.Get<DetectedCarPrinter>());
            AddProcessor(_kernel.Get<RegisteredCarFilter>());
            AddProcessor(_kernel.Get<WeightningGetter>());
            AddProcessor(_kernel.Get<CarAreaByCameraSetter>());
            AddProcessor(_kernel.Get<CurrentStateGetter>());
            AddProcessor(_kernel.Get<StateFixer>());
            //AddProcessor(_kernel.Get<StatesFilter>());
            AddProcessor(_kernel.Get<WaitingListsGetter>());
            AddProcessor(_kernel.Get<AccessTypeGetter>());
            AddProcessor(_kernel.Get<PurposesGetter>());
            AddProcessor(stateSwitchers);
            AddProcessor(_kernel.Get<OpenBarrierProcessor>());
        }

        private void StateSwitchers_AllSkipped(object? sender, CarInfo e)
        {
            logger.Error($"({e.Camera.Name})({e.NormalizedPlateNumber}): Статус не был изменен. Текущий статус: ({e.State.Name})");
        }

        protected override void OnException(IProcessor<CarInfo> proc, ProcessorExceptionArgs<CarInfo> args)
        {
            base.OnException(proc, args);
            logger.Error(args.Ex, $"({args.Info.Camera.Name})({args.Info.NormalizedPlateNumber})({args.Info.State}) Исключение в процессоре {proc.GetType().Name}. Ex.Message: {args.Ex.Message}");
        }
    }
}
