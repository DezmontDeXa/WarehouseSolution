using NLog;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class BlockCarAfterLoadingUnloadingProcessor : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public BlockCarAfterLoadingUnloadingProcessor(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if(info.State.TypeName == nameof(OnEnterState))
            {
                var carInDb = dbMethods.GetCarById(info.Car.Id);
                if(carInDb.FirstWeighingCompleted && !carInDb.SecondWeighingCompleted)
                {
                    dbMethods.SetCarState(info.Car.Id, new AwaitingSecondWeighingState().Id);
                    Logger.Info($"Статус изменен на {new AwaitingSecondWeighingState().Name}");
                    return ProcessorResult.Finish;
                }    
            }

            return ProcessorResult.Next;
        }
    }
}