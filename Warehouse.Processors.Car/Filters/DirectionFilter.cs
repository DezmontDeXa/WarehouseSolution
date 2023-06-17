using NLog;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Filters
{
    public class DirectionFilter : CarInfoProcessorBase
    {
        public DirectionFilter(ILogger logger) : base(logger)
        {

        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var camera = info.Camera;
            var direction = info.MoveDirectionString;

            if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
            {
                Logger.Trace(BuildLogMessage(info, $"Не верное направление движения. Ожидалось: {camera.Direction}. Направление: {direction}. Обработка прервана."));
                return ProcessorResult.Finish;
            }

            return ProcessorResult.Next;
        }
    }
}