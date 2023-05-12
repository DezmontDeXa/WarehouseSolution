using Microsoft.EntityFrameworkCore;
using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;

namespace CheckPointControl.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public ObservableCollection<Car> AwaitingCars => GetCarsByState<AwaitingState>();
        public ObservableCollection<Car> OnAreaCars => GetCarsOnArea();
        public ObservableCollection<Car> OnLoadingCars => GetCarsByState<LoadingState>();
        public ObservableCollection<Car> ExitPassGrantedCars => GetCarsByState<ExitPassGrantedState>();
        public ObservableCollection<Car> RequiredInspectionCars => GetCarsInspection();

        private List<Car> _cars = new List<Car>();
        private readonly AreaService areaService;

        public MainViewModel(AreaService areaService)
        {
            Task.Run(UpdateLists);
            this.areaService = areaService;
        }

        private void UpdateLists()
        {
            while (true)
            {
                Task.Delay(1000).Wait();

                using (var db = new WarehouseContext())
                {
                    var allCars = db.Cars.Include(x=>x.Area).Include(x=>x.CarState).Where(x => x.Area.Id == areaService.SelectedArea.Id).ToList();

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

                    RaisePropertyChanged(nameof(AwaitingCars));
                    RaisePropertyChanged(nameof(OnAreaCars));
                    RaisePropertyChanged(nameof(OnLoadingCars));
                    RaisePropertyChanged(nameof(ExitPassGrantedCars));
                    RaisePropertyChanged(nameof(RequiredInspectionCars));
                }
            }
        }

        private ObservableCollection<Car> GetCarsByState<T>() where T : CarStateBase
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.CarState.TypeName == typeof(T).Name));
        }

        private ObservableCollection<Car> GetCarsOnArea()
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.Area.Id == areaService.SelectedArea.Id));
        }

        private ObservableCollection<Car> GetCarsInspection()
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.IsInspectionRequired));
        }
    }
}
