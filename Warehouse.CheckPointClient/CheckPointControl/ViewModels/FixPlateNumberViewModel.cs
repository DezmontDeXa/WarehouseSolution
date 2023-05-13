using CheckPointControl.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Xaml.Behaviors.Core;
using NLog;
using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Warehouse.CheckPointClient.Services;
using Warehouse.Services;

namespace CheckPointControl.ViewModels
{
    public class FixPlateNumberViewModel : BindableBase
    {
        public List<Car> FilteredCars { get => filteredCars; set => SetProperty(ref filteredCars, value); }
        public Car SelectedCar { get => selectedCar; set { SetProperty(ref selectedCar, value); RaisePropertyChanged(nameof(HasSelectedCar)); Similars = selectedCar?.PlateNumberSimilars; } }
        public string Filter { get => filter; set { SetProperty(ref filter, value); UpdateFilteredCars(); } }
        public ICommand FixNumberCommand => fixNumberCommand ??= new ActionCommand(FixNumber);
        public string FixReason { get => fixReason; set => SetProperty(ref fixReason, value); }
        public bool HasSelectedCar => SelectedCar != null;
        public string Similars { get => similars; set => SetProperty(ref similars, value); }


        private List<Car> filteredCars;
        private Car selectedCar;
        private string filter;
        private readonly CarsService carService;
        private readonly ILogger logger;
        private readonly AutorizationService authService;
        private readonly AreaService areaService;
        private readonly IBarriersService barrierService;
        private string fixReason;
        private ActionCommand fixNumberCommand;
        private string similars;

        public FixPlateNumberViewModel(CarsService carService, ILogger logger, AutorizationService authService, AreaService areaService, IBarriersService barrierService)
        {
            this.carService = carService;
            this.logger = logger;
            this.authService = authService;
            this.areaService = areaService;
            this.barrierService = barrierService;
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
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.First(x => x.Id == selectedCar.Id);
                car.PlateNumberSimilars = Similars;
                db.SaveChanges();

                OpenBarrier(db);
            }

            logger.Warn($"Для машины с номером {selectedCar.PlateNumberForward} были изменены синонимы номера и открыт шлагбаум на {areaService.SelectedArea.Name}. (Пользователь: {authService.AuthorizedUserLogin}) по причине: {FixReason}.");

            FixReason = null;
            SelectedCar = null;
        }

        private void OpenBarrier(WarehouseContext db)
        {
            var barrier = db.BarrierInfos.Include(x => x.Area).FirstOrDefault(x => x.Area.Id == areaService.SelectedArea.Id);
            if (barrier == null)
                logger.Error($"Не удалось открыть шлагбаум на территории {areaService.SelectedArea.Name}. Шлагбаум не найден.");
            else
                barrierService.Switch(barrier, SimpleBarrierService.BarrierCommand.Open);
        }
    }
}
