using NLog;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class AccessTypeGetter : CarInfoProcessorBase
    {
        public AccessTypeGetter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var topList = info.WaitingLists.OrderByDescending(x => x.AccessType).FirstOrDefault();
            info.AccessType = topList.AccessType;
            Logger.Trace(BuildLogMessage(info, $"Тип доступа: {info.AccessType}"));
            return ProcessorResult.Next;
        }
    }
}