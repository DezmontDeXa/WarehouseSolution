using Microsoft.EntityFrameworkCore;
using Services;
using SharedLibrary.DataBaseModels;
using System;
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
                        //Add car
                        if (existCar == null)
                        {
                            _cars.Add(car);
                        }
                        // Update Car
                        else
                        {
                            if (existCar.AreaId != car.AreaId)
                            {
                                existCar.Area = car.Area;
                                existCar.AreaId = car.AreaId;
                            }
                            if (existCar.CarState.Id != car.CarState.Id)
                            {
                                existCar.CarState = car.CarState;
                                existCar.CarState.Id = car.CarState.Id;
                            }
                            if (existCar.IsInspectionRequired != car.IsInspectionRequired)
                                existCar.IsInspectionRequired = car.IsInspectionRequired;
                        }
                    }

                    // Remove not exist car
                    foreach (var car in _cars.ToArray())
                        if (!allCars.Any(x => x.Id == car.Id))
                            _cars.Remove(car);

                }

                CarsUpdated?.Invoke(this, Cars);
            }
        }

    }
}
