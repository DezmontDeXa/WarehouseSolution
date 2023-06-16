using NLog;
using Warehouse.CarStates;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.Processors.Car.Core
{
    public abstract class CarInfoProcessorBase : IProcessor<CarInfo>
    {
        protected ILogger Logger { get; }

        public CarInfoProcessorBase(ILogger logger)
        {
            Logger = logger;
        }

        public ProcessorResult Process(CarInfo info)
        {
            try
            {
                return Action(info);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ProcessorResult.Finish;
            }
        }

        protected abstract ProcessorResult Action(CarInfo info);

        protected void ChangeStatus(IWarehouseDataBaseMethods dbMethods, int carId, CarStateBase state)
        {
            dbMethods.SetCarState(carId, state.Id);
            Logger.Info($"Статус изменен на {state.Name}");
        }
    }
}