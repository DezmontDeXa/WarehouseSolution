using NLog;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.WaitingListsServices
{
    /// <summary>
    /// Check platenumber has access
    /// </summary>
    public class WaitingListsService : IWaitingListsService
    {
        private readonly ILogger _logger;
        private readonly IFindCarService _findCarService;
        private readonly IWarehouseDataBaseMethods dbMethods;

        public WaitingListsService(ILogger logger, IFindCarService findCarService, IWarehouseDataBaseMethods dbMethods)
        {
            _logger = logger;
            _findCarService = findCarService;
            this.dbMethods = dbMethods;
        }

        public ICarAccessInfo GetAccessTypeInfo(string plateNumber)
        {
            try
            {
                var car = _findCarService.FindCar(plateNumber);// FindCar(plateNumber);
                if (car == null)
                    return new CarAccessInfo(null, null);

                car = dbMethods.GetCarsWithWaitingLists().First(x => x.Id == car.Id);
                var includs = car.WaitingLists.ToList();

                if (includs.Count == 0)
                    return new CarAccessInfo(car, null);

                return new CarAccessInfo(car, includs);
            }
            catch (Exception ex)
            {
                _logger.Error($"Failed on getting car access type for plateNumber {plateNumber}. Exception: {ex}");
                throw;
            }
        }
    }
}