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

        public CarsList ByState(params int[] stateIds)
        {
            var cars = new List<Car>();
            foreach (var car in this)
            {
                foreach (var id in stateIds)
                {
                    if (car.CarStateId == id)
                        cars.Add(car);
                }
            }
            var list = new CarsList(cars);
            return list;
        }

        public CarsList ByArea(Area area)
        {
            return new CarsList(this.Where(x => x.AreaId == area.Id));
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
