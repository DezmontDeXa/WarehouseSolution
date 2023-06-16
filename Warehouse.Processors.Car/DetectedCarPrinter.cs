using NLog;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class DetectedCarPrinter : CarInfoProcessorBase
    {
        public DetectedCarPrinter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            Logger.Info($"Обнаружена машина. Номер: {info.RecognizedPlateNumber}. Камера: {info.Camera.Name}");
            return ProcessorResult.Next;
        }
    }
}