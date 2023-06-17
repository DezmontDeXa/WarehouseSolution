using NLog;
using Warehouse.Interfaces.WaitingListServices;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Getters
{
    public class PurposesGetter : CarInfoProcessorBase
    {
        public PurposesGetter(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            info.Purposes = info.WaitingLists.Select(x => x.PurposeOfArrival ?? "").ToList();
            Logger.Trace(BuildLogMessage(info, $"Причины заезда: {string.Join(", ", info.Purposes.Distinct())}"));
            return ProcessorResult.Next;
        }
    }
}