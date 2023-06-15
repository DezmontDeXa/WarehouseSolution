using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.Processors.Car
{
    public class DirectionFilter : IProcessor<CarInfo>
    {
        public ProcessorResult Process(CarInfo info)
        {
            if (info.Camera.Direction == info.DetectedDirection)
                return ProcessorResult.Next;

            // TODO: incorrect direction
            return ProcessorResult.Finish;
        }
    }

    public class StatesFilter : DbProcessorBase
    {
        private readonly IWarehouseConfigDataBaseMethods _configDbMethods;

        public StatesFilter(IWarehouseDataBaseMethods dbmethods, IWarehouseConfigDataBaseMethods configDbMethods) : base(dbmethods)
        {
            _configDbMethods = configDbMethods;
        }

        public override ProcessorResult Process(CarInfo info)
        {
            //_configDbMethods.GetCameraExpectedStates(info.Camera.ExpectedStates);
            return ProcessorResult.Finish;
        }
    }

}