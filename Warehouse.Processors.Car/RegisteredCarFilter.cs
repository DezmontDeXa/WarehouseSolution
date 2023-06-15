using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.RusificationServices;

namespace Warehouse.Processors.Car
{
    public class RegisteredCarFilter : IProcessor<CarInfo>
    {
        private readonly IRussificationService _ruService;
        private readonly IFindCarService _findCarService;

        public RegisteredCarFilter(IRussificationService ruService, IFindCarService findCarService)
        {
            _ruService = ruService;
            _findCarService = findCarService;
        }

        public ProcessorResult Process(CarInfo info)
        {
            info.NormalizedPlateNumber = _ruService.ToRu(info.RecognizedPlateNumber).ToUpper();
            var car = _findCarService.FindCar(info.NormalizedPlateNumber);
            if(car==null)
            {
                // TODO: Car not found
                return ProcessorResult.Finish;
            }
            info.Car = car;
            return ProcessorResult.Next;
        }
    }
}