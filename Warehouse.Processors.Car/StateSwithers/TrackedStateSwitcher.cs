using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.StateSwithers
{
    public class TrackedStateSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public TrackedStateSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (info.AccessType != AccessType.Tracked)
                return ProcessorResult.Next;

            switch (info.State.TypeName)
            {
                case nameof(AwaitingState):
                    ChangeStatus(dbMethods, info, new OnEnterState());
                    return ProcessorResult.Finish;

                case nameof(OnEnterState):
                    ChangeStatus(dbMethods, info, new AwaitingFirstWeighingState());
                    return ProcessorResult.Finish;

                case nameof(AwaitingFirstWeighingState):
                    ChangeStatus(dbMethods, info, new WeighingState());
                    return ProcessorResult.Finish;


                case nameof(ExitingState):
                    ChangeStatus(dbMethods, info, new FinishState());
                    return ProcessorResult.Finish;

            }

            return ProcessorResult.Next;
        }
    }
}