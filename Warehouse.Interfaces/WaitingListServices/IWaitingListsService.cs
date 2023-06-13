namespace Warehouse.Interfaces.WaitingListServices
{
    public interface IWaitingListsService
    {
        ICarAccessInfo GetAccessTypeInfo(string plateNumber);
    }
}