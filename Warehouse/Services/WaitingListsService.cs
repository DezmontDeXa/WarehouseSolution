using NLog;
using Warehouse.SharedLibrary;

namespace Warehouse.Services
{
    /// <summary>
    /// Check platenumber has access
    /// </summary>
    public class WaitingListsService
    {
        private readonly WarehouseContext _db;
        private readonly ILogger _logger;

        public WaitingListsService(WarehouseContext db, ILogger logger)
        {
            _db = db;
            _logger = logger;
        }

        public AccessGrantType GetAccessType(string plateNumber)
        {
            try
            {
                var car = FindCar(plateNumber);
                var includs = new List<WaitingList>();

                foreach (var waitingList in _db.WaitingLists)
                {
                    foreach (var pair in _db.WaitingListToCars)
                    {
                        if (pair.CarId == car.Id)
                        {
                            includs.Add(waitingList);
                            break;
                        }
                    }
                }

                if (includs.Count == 0)
                    return AccessGrantType.None;

                return (AccessGrantType)includs.Select(x => x.AccessGrantType).Max();
            }catch(Exception ex)
            {
                _logger.Error($"Failed on getting car access type for plateNumber {plateNumber}. Exception: {ex}");
                throw;
            }
        }

        private Car FindCar(string plateNumber)
        {
            foreach (var car in _db.Cars)
            {
                if (car == null) continue;
                if (car.PlateNumberForward == plateNumber)
                    return car;
                if (car.PlateNumberBackward == plateNumber)
                    return car;
                foreach (var similar in car.PlateNumberSimilars.Split(new char[] {','}))
                {
                    if (similar == plateNumber)
                        return car;
                }
            }

            return null;
        }
    }

    public enum AccessGrantType
    {
        None = -1,
        Free = 0,
        Tracked = 1
    }
}
