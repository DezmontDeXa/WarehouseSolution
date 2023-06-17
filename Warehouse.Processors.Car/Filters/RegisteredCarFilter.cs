using NLog;
using NLog.Fluent;
using Warehouse.DataBase.Models.Config;
using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.Filters
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
            if (car == null)
            {
                Logger.Error(BuildLogMessage(info, $"Неизвестная машина ({info.NormalizedPlateNumber}). Обработка прервана."));
                return ProcessorResult.Finish;
            }
            info.Car = car;
            Logger.Trace(BuildLogMessage(info, $"Машина найдена в базе"));
            return ProcessorResult.Next;
        }
    }
}