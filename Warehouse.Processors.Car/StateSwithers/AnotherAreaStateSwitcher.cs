using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.StateSwithers
{
    public class AnotherAreaStateSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbmethods;

        public AnotherAreaStateSwitcher(IWarehouseDataBaseMethods dbmethods, ILogger logger) : base(logger)
        {
            this.dbmethods = dbmethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            switch (info.State.TypeName)
            {
                case nameof(ChangingAreaState):
                    if (!info.AnotherAreaProgress)
                    {
                        info.AnotherAreaProgress = true;
                        ChangeStatus(dbmethods, info, new LoadingState());
                    }
                    else
                    {
                        ChangeStatus(dbmethods, info, new OnEnterState()); //TODO: or unloading
                    }
                    return ProcessorResult.Finish;

                case nameof(LoadingState):
                    ChangeStatus(dbmethods, info, new ChangingAreaState());
                    return ProcessorResult.Finish;

                case nameof(UnloadingState):
                    ChangeStatus(dbmethods, info, new ChangingAreaState());
                    return ProcessorResult.Finish;
            }

            return ProcessorResult.Next;
        }
    }
}