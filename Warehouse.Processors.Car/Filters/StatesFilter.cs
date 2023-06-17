using NLog;
using Warehouse.DataBase.Models.Config;
using Warehouse.DataBase.Models.Main;
using Warehouse.ExprectedStates;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Filters
{
    public class StatesFilter : CarInfoProcessorBase
    {
        public StatesFilter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (ExpectedStatesConfig.IsExpectedState(info.Camera.RoleId, info.State.Id))
            {
                return ProcessorResult.Next;
            }
            else
            {
                Logger.Error($"Неожиданный стутус. Статус: {info.State.Name}. Обработка прервана.");
                return ProcessorResult.Finish;
            }
        }
    }
}