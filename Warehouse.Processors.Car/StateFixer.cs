using NLog;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates;
using Warehouse.CarStates.Implements;
using Warehouse.ExprectedStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class StateFixer : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public StateFixer(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (ExpectedStatesConfig.IsExpectedState(info.Camera.RoleId, info.State.Id))
            {
                return ProcessorResult.Next;
            }
            else
            {
                if (info.Camera.RoleId == new BeforeEnterRole().Id)
                    FixStatus(dbMethods, info, new AwaitingState());

                if (info.Camera.RoleId == new AfterEnterRole().Id)
                    FixStatus(dbMethods, info, new OnEnterState());

                if (info.Camera.RoleId == new OnWeightingRole().Id)
                {
                    if(!info.HasFirstWeightning)
                        FixStatus(dbMethods, info, new AwaitingFirstWeighingState());
                    else if (!info.HasSecondWeightning)
                        FixStatus(dbMethods, info, new AwaitingSecondWeighingState());
                    else
                        FixStatus(dbMethods, info, new AwaitingFirstWeighingState());
                }

                if (info.Camera.RoleId == new ExitRole().Id)
                    FixStatus(dbMethods, info, new ExitPassGrantedState());


                if (info.Camera.RoleId == new EnterRole().Id)
                    FixStatus(dbMethods, info, new ChangingAreaState());

                if (info.Camera.RoleId == new ExitFromAnotherAreaRole().Id)
                    FixStatus(dbMethods, info, new LoadingState());

                return ProcessorResult.Next;
            }
        }

        private void FixStatus(IWarehouseDataBaseMethods dbMethods, CarInfo info, CarStateBase state)
        {
            dbMethods.SetCarState(info.Car.Id, state.Id);
            Logger.Warn(BuildLogMessage(info, $"Статус исправлен c {info.State.Name} на {state.Name}"));
            info.State = BuildCarStateType(state);
        }
    }
}