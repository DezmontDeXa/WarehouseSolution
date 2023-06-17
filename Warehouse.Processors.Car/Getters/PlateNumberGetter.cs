using NLog;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class PlateNumberGetter : CarInfoProcessorBase
    {
        public PlateNumberGetter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            info.RecognizedPlateNumber = ParsePlateNumber(info.AnprBlock);
            return ProcessorResult.Next;
        }

        private static string ParsePlateNumber(ICameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]?["licensePlate"]?.InnerText ?? "";
        }
    }
}