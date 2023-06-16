using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class ChangeCarAreaProcessor : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbmethods;

        public ChangeCarAreaProcessor(IWarehouseDataBaseMethods dbmethods, ILogger logger) : base(logger)
        {
            this.dbmethods = dbmethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            dbmethods.SetCarArea(info.Car, info.Camera.AreaId);
            Logger.Trace($"Территория машины изменена на территорию камеры {info.Camera.Name}");
            return ProcessorResult.Next;
        }
    }
}