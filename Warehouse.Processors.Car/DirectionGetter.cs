using NLog;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class DirectionGetter : CarInfoProcessorBase
    {
        public DirectionGetter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            info.MoveDirectionString = ParseDirection(info.AnprBlock);
            return ProcessorResult.Next;
        }

        private static string ParseDirection(ICameraNotifyBlock block)
        {
            var direction = block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
            return direction;
        }
    }
}