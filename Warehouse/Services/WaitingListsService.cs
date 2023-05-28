using Microsoft.EntityFrameworkCore;
using NLog;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    /// <summary>
    /// Check platenumber has access
    /// </summary>
    public class WaitingListsService
    {
        private readonly ILogger _logger;
        private readonly FuzzyFindCarService _findCarService;

        public WaitingListsService(ILogger logger, FuzzyFindCarService findCarService)
        {
            _logger = logger;
            _findCarService = findCarService;
        }

        public CarAccessInfo GetAccessTypeInfo(string plateNumber)
        {
            try
            {
                var car = _findCarService.FindCar(plateNumber);// FindCar(plateNumber);
                if (car == null)
                    return new CarAccessInfo(null, null);

                using(var db  = new WarehouseContext())
                    car = db.Cars
                        .Include(x => x.WaitingLists)
                        .First(x => x.Id == car.Id);

                var includs = car.WaitingLists.OrderByDescending(x=>x.AccessGrantType).ToList();

                if (includs.Count == 0)
                    return new CarAccessInfo(car, null);

                return new CarAccessInfo(car, includs.First());
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed on getting car access type for plateNumber {plateNumber}. Exception: {ex}");
                throw;
            }
        }

        private Car FindCar(string plateNumber)
        {
            using (var db = new WarehouseContext())
            {
                //var cars = db.Cars.Include(x => x.WaitingLists).Include(x => x.CarState);

                foreach (var car in db.Cars.Include(x => x.WaitingLists))
                {
                    if (car == null) continue;
                    if (car.PlateNumberForward.ToLower() == plateNumber.ToLower())
                        return car;
                    if (car.PlateNumberBackward.ToLower() == plateNumber.ToLower())
                        return car;

                    if (car.PlateNumberSimilars != null)
                        foreach (var similar in car.PlateNumberSimilars.Split(new char[] { ',' }))
                            if (similar.ToLower() == plateNumber.ToLower())
                                return car;
                }

                return null;
            }
        }
    }

    public class CarAccessInfo
    {
        public CarAccessInfo(Car car, WaitingList list)
        {
            Car = car;
            List = list;
            AccessType = list?.AccessGrantType;
        }

        public Car Car { get; }
        public WaitingList? List { get; }
        public AccessGrantType? AccessType { get; }
    }

}
