using NLog;
using Warehouse.DataBase.Models.Config;
using Warehouse.Interfaces.WaitingListServices;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class AccessTypeSelector : CarInfoProcessorBase
    {
        public AccessTypeSelector(ILogger logger) : base(logger)
        {
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var topList = info.WaitingLists.OrderByDescending(x => x.AccessType).FirstOrDefault();
            info.AccessType = topList.AccessType;
            Logger.Trace($"Тип доступа: {info.AccessType}");
            return ProcessorResult.Next;
        }
    }
}