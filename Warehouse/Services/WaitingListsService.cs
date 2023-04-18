namespace Warehouse.Services
{
    /// <summary>
    /// Check platenumber has access
    /// </summary>
    public class WaitingListsService
    {
        private readonly WarehouseContext _db;

        public WaitingListsService(WarehouseContext db)
        {
            _db = db;
        }

        public bool CanAccess(string plateNumber)
        {
            //TODO: Проверить, есть ли plateNumber в списках разрешенных/ожидаемых 
            return true;
        }
    }
}
