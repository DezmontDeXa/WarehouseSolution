using Warehouse.Interfaces.DataBase;

namespace Warehouse.Interfaces.WaitingListServices
{
    public interface ICarAccessInfo
    {
        List<IWaitingList> AllIncludeLists { get; }
        ICar Car { get; }
        AccessGrantType? TopAccessType { get; }
        IWaitingList? TopAccessTypeList { get; }
        PurposeOfArrival TopPurposeOfArrival { get; }
    }
}