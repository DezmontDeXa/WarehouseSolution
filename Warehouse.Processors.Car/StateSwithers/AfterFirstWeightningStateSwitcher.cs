using NLog;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.StateSwithers
{
    public class AfterFirstWeightningStateSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbmethods;

        public AfterFirstWeightningStateSwitcher(IWarehouseDataBaseMethods dbmethods, ILogger logger) : base(logger)
        {
            this.dbmethods = dbmethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (info.Camera.RoleId == new ExitRole().Id)
                if (info.State.TypeName == nameof(ExitingForChangeAreaState))
                {
                    ChangeStatus(dbmethods, info, new ChangingAreaState());
                    dbmethods.SetCarState(info.Car.Id, new ChangingAreaState().Id);
                    return ProcessorResult.Finish;
                }

            if (info.Camera.RoleId == new OnWeightingRole().Id)
                if (info.State.TypeName == nameof(LoadingState))
                {
                    ChangeStatus(dbmethods, info, new WeighingState());
                    return ProcessorResult.Finish;
                }

            if (info.Camera.RoleId == new OnWeightingRole().Id)
                if (info.State.TypeName == nameof(UnloadingState))
                {
                    ChangeStatus(dbmethods, info, new WeighingState());
                    return ProcessorResult.Finish;
                }

            return ProcessorResult.Next;
        }
    }
}