using NLog;
using Warehouse.DataBase.Models.Config;
using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car
{
    public class RegisteredCarFilter : CarInfoProcessorBase
    {
        private readonly IRussificationService _ruService;
        private readonly IFindCarService _findCarService;

        public RegisteredCarFilter(IRussificationService ruService, IFindCarService findCarService, ILogger logger) : base(logger)
        {
            _ruService = ruService;
            _findCarService = findCarService;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            info.NormalizedPlateNumber = _ruService.ToRu(info.RecognizedPlateNumber).ToUpper();
            var car = _findCarService.FindCar(info.NormalizedPlateNumber);
            if(car==null)
            {
                Logger.Error($"Машина не найдена в базе. Обработка прервана.");
                return ProcessorResult.Finish;
            }
            info.Car = car;
            Logger.Trace($"Машина найдена в базе");
            return ProcessorResult.Next;
        }
    }
}