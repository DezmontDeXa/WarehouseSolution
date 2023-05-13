using Microsoft.EntityFrameworkCore;
using Services;
using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPointControl.Services
{
    public class CarsService
    {
        public CarsList Cars
        {
            get => _cars; 
            private set
            {
                _cars = value; 
            }
        }

        public event EventHandler<CarsList> CarsUpdated;

        private CarsList _cars;

        public CarsService()
        {
            Cars = new CarsList();
            Task.Run(UpdateLists);
        }

        private void UpdateLists()
        {
            while (true)
            {
                Task.Delay(1000).Wait();

                using (var db = new WarehouseContext())
                {
                    var allCars = db.Cars.Include(x => x.Area).Include(x => x.CarState).ToList();

                    foreach (var car in allCars)
                    {
                        var existCar = _cars.FirstOrDefault(x => x.Id == car.Id);
                        if (existCar == null)
                        {
                            _cars.Add(car);
                            existCar = car;
                        }

                        if (existCar.CarState.Id != car.CarState.Id)
                            existCar.CarState = car.CarState;

                        if (existCar.IsInspectionRequired != car.IsInspectionRequired)
                            existCar.IsInspectionRequired = car.IsInspectionRequired;
                    }

                    foreach (var car in _cars.ToArray())
                        if (!allCars.Any(x => x.Id == car.Id))
                            _cars.Remove(car);

                }

                CarsUpdated?.Invoke(this, Cars);
            }
        }

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

            public CarsList ByArea( Area area)
            {
                return new CarsList(this.Where(x => x.Area?.Id == area?.Id));
            }

            public CarsList WithInspectionRequired()
            {
                return new CarsList(this.Where(x => x.IsInspectionRequired));
            }
        }
    }
}
