using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class WeightningGetter : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public WeightningGetter(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var dbCar = dbMethods.GetCarById(info.Car.Id);
            info.HasFirstWeightning = dbCar.FirstWeighingCompleted;
            info.HasSecondWeightning = dbCar.SecondWeighingCompleted;
            return ProcessorResult.Next;
        }
    }
}