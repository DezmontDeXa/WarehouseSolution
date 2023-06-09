using SharedLibrary.DataBaseModels;

namespace WaitingListsService
{

    public class CarAccessInfo
    {
        public CarAccessInfo(Car car, List<WaitingList> lists)
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

        public Car Car { get; }
        public List<WaitingList> AllIncludeLists { get; }
        public WaitingList? TopAccessTypeList { get; }
        public AccessGrantType? TopAccessType { get; }
        public PurposeOfArrival TopPurposeOfArrival { get; }
    }
}