using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.WaitingListsServices
{
    public class CarAccessInfo : ICarAccessInfo
    {
        public CarAccessInfo(ICar car, List<IWaitingList> lists)
        {
            Car = car;
            AllIncludeLists = lists;
            TopAccessTypeList = lists?.OrderByDescending(x => x.AccessGrantType)?.FirstOrDefault();
            TopAccessType = TopAccessTypeList?.AccessGrantType;

            switch (TopAccessTypeList?.PurposeOfArrival?.ToLower()?.Replace(" ", ""))
            {
                case "погрузка":
                    TopPurposeOfArrival = PurposeOfArrival.Loading;
                    break;
                case "разгрузка":
                    TopPurposeOfArrival = PurposeOfArrival.Unloading;
                    break;
                case "взятие проб":
                    TopPurposeOfArrival = PurposeOfArrival.Sampling;
                    break;
            }
        }

        public ICar Car { get; }
        public List<IWaitingList> AllIncludeLists { get; }
        public IWaitingList? TopAccessTypeList { get; }
        public AccessGrantType? TopAccessType { get; }
        public PurposeOfArrival TopPurposeOfArrival { get; }
    }
}