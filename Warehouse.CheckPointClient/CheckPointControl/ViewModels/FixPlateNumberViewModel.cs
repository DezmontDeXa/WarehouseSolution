using CheckPointControl.Services;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Mvvm;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace CheckPointControl.ViewModels
{
    public class FixPlateNumberViewModel : BindableBase
    {
        public List<Car> FilteredCars { get => filteredCars; set => SetProperty(ref filteredCars, value); }
        public Car SelectedCar { get => selectedCar; set { SetProperty(ref selectedCar, value); RaisePropertyChanged(nameof(HasSelectedCar)); Similars = selectedCar.PlateNumberSimilars; } }
        public string Filter { get => filter; set { SetProperty(ref filter, value); UpdateFilteredCars(); } }
        public ICommand FixNumberCommand => fixNumberCommand ??= new ActionCommand(FixNumber);
        public string FixReason { get => fixReason; set => SetProperty(ref fixReason, value); }
        public bool HasSelectedCar => SelectedCar != null;
        public string Similars { get => similars; set => SetProperty(ref similars, value); }


        private List<Car> filteredCars;
        private Car selectedCar;
        private string filter;
        private readonly CarsService carService;
        private string fixReason;
        private ActionCommand fixNumberCommand;
        private string similars;

        public FixPlateNumberViewModel(CarsService carService)
        {
            this.carService = carService;
            carService.CarsUpdated += OnCarsUpdated;
        }

        private void OnCarsUpdated(object sender, CarsService.CarsList e)
        {
            UpdateFilteredCars();
        }

        private void UpdateFilteredCars()
        {
            if (string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter))
                FilteredCars = null;
            else
                FilteredCars = carService.Cars.Where(x => x.PlateNumberForward.ToUpper().StartsWith(filter.ToUpper())).ToList();
        }

        private void FixNumber()
        {
            using(var db = new WarehouseContext())
            {
                var car = db.Cars.First(x => x.Id == selectedCar.Id);
                car.PlateNumberSimilars = Similars;
                db.SaveChanges();
            }

            FixReason = null;
            SelectedCar = null;
        }
    }
}
