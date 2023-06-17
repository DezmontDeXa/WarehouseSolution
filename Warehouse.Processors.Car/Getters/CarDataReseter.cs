using NLog;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class CarDataReseter : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public CarDataReseter(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (info.Car is null)
                throw new ArgumentNullException(nameof(info.Car));

            dbMethods.SetCarInspectionRequired(info.Car, false);
            dbMethods.SetCarArea(info.Car, null);
            dbMethods.SetCarFirstWeightning(info.Car, false);
            dbMethods.SetCarSecondWeightning(info.Car, false);

            Logger.Trace(BuildLogMessage(info, $"Машина уехала: сбрасываем территорию и веса"));
            return ProcessorResult.Next;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if(info.Camera.RoleId == new ExitRole().Id)
            {
                if (info.State == new AwaitingState())
                    return true;

                if (info.State == new FinishState())
                    return true;
            }
            return false;
        }
    }
}