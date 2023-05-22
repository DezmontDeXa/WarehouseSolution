using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Linq;

namespace CheckPointControl.Services
{
    public class CarsList : List<Car>
    {
        public CarsList(IEnumerable<Car> cars) : base(cars)
        {

        }

        public CarsList()
        {
        }

        public CarsList FilterCars()
        {
            return this;
        }

        public CarsList ByState<T>()
        {
            return new CarsList(this.Where(x => x.CarState.TypeName == typeof(T).Name));
        }

        public CarsList ByState<T1, T2>()
        {
            return new CarsList(this.Where(x => x.CarState.TypeName == typeof(T1).Name || x.CarState.TypeName == typeof(T2).Name));
        }

        public CarsList ByArea(Area area)
        {
            return new CarsList(this.Where(x => x.Area?.Id == area?.Id));
        }
        public CarsList ByTargetArea(Area area)
        {
            return new CarsList(this.Where(x => x.TargetAreaId == area?.Id));
        }

        public CarsList WithInspectionRequired()
        {
            return new CarsList(this.Where(x => x.IsInspectionRequired));
        }
    }
}
