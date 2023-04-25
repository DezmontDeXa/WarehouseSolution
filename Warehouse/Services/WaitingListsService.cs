using Microsoft.EntityFrameworkCore;
using NLog;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Services
{
    /// <summary>
    /// Check platenumber has access
    /// </summary>
    public class WaitingListsService : IDisposable
    {
        private readonly WarehouseContext _db;
        private readonly ILogger _logger;

        public WaitingListsService(WarehouseContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public CarAccessInfo GetAccessTypeInfo(string plateNumber)
        {
            try
            {
                var car = FindCar(plateNumber);
                if (car == null)
                    return new CarAccessInfo(null, null);

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
            foreach (var car in _db.Cars.Include(x => x.WaitingLists))
            {
                if (car == null) continue;
                if (car.PlateNumberForward == plateNumber)
                    return car;
                if (car.PlateNumberBackward == plateNumber)
                    return car;

                if (car.PlateNumberSimilars != null)
                    foreach (var similar in car.PlateNumberSimilars.Split(new char[] { ',' }))
                        if (similar == plateNumber)
                            return car;
            }

            return null;
        }

        public void Dispose()
        {
            _db.Dispose();
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
