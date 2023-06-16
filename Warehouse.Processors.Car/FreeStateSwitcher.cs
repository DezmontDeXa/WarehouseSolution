using NLog;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{

    public class FreeStateSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbmethods;

        public FreeStateSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbmethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if(info.AccessType != AccessType.Free)
                return ProcessorResult.Next;

            switch (info.State.TypeName)
            {
                case nameof(AwaitingState):
                    ChangeStatus(dbmethods, info.Car.Id, new OnEnterState());
                    return ProcessorResult.Finish;
                case nameof(OnEnterState):
                    ChangeStatus(dbmethods, info.Car.Id, new ExitPassGrantedState());
                    return ProcessorResult.Finish;
                case nameof(ExitPassGrantedState):
                    if (info.Camera.RoleId == new OnWeightingRole().Id)
                        ChangeStatus(dbmethods, info.Car.Id, new WeighingState());
                    else if (info.Camera.RoleId == new ExitRole().Id)
                        ChangeStatus(dbmethods, info.Car.Id, new AwaitingState());
                    else
                        return ProcessorResult.Next;

                    return ProcessorResult.Finish;
            }

            return ProcessorResult.Next;
        }
    }
}