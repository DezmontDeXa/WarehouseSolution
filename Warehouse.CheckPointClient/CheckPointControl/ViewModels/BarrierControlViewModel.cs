using CheckPointControl.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using SharedLibrary.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Warehouse.CheckPointClient.Services;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace CheckPointControl.ViewModels
{
    public class BarrierControlViewModel : BindableBase
    {
        public ICollectionView FilteredCars { get; set; }
        public Car SelectedCar
        {
            get => selectedCar;
            set
            {
                SetProperty(ref selectedCar, value);
                RaisePropertyChanged(nameof(HasSelectedCar));
                RaisePropertyChanged(nameof(RequiredReason));
                SelectedReasonIndex = 0;
                OpenCommand.RaiseCanExecuteChanged();

                if (value != null)
                {
                    SetProperty(ref filter, value.PlateNumberForward);
                    RaisePropertyChanged(nameof(Filter));
                }
            }
        }
        public string Filter
        {
            get => filter;
            set
            {
                SetProperty(ref filter, value);
                SelectedReasonIndex = 0;
                OpenCommand.RaiseCanExecuteChanged();
                ApplyFilter();
            }
        }
        public bool HasSelectedCar
            => SelectedCar != null;
        public bool RequiredReason
        {
            get
            {
                if (SelectedCar == null) 
                    return true;
                if (SelectedCar.AreaId == null) 
                    return true;
                if (SelectedCar.AreaId != areaService.SelectedArea.Id) 
                    return true;
                if (SelectedCar.CarStateId == null)
                    return true;
                if (SelectedCar.CarStateId != new AwaitingState().Id && SelectedCar.CarStateId != new ExitPassGrantedState().Id) 
                    return true;

                return false;
            }
        }
        public int SelectedReasonIndex
        {
            get => selectedReasonIndex;
            set
            {
                SetProperty(ref selectedReasonIndex, value);
                OpenCommand.RaiseCanExecuteChanged();
            }
        }
        public DelegateCommand OpenCommand
            => openCommand ??= new DelegateCommand(Open, CanOpen);
        public List<string> PassReasons
        {
            get => passReasons;
            private set => passReasons = value;
        }


        private Car selectedCar;
        private string filter;
        private readonly CarsService carService;
        private readonly ILogger logger;
        private readonly AutorizationService authService;
        private readonly AreaService areaService;
        private readonly IBarriersService barrierService;
        private readonly DetectedPlateNumberService detectedPlateNumberService;
        private int selectedReasonIndex;
        private DelegateCommand openCommand;
        private List<string> passReasons = new List<string>()
        {
            "Не указана",
            "Взвешиванеие", "Разгрузка","Погрузка","Легковая"
        };

        public BarrierControlViewModel(
            CarsService carService,
            ILogger logger, 
            AutorizationService authService, 
            AreaService areaService, 
            IBarriersService barrierService, 
            DetectedPlateNumberService detectedPlateNumberService)
        {
            this.logger = logger;
            this.authService = authService;
            this.areaService = areaService;
            this.barrierService = barrierService;
            this.detectedPlateNumberService = detectedPlateNumberService;
            this.carService = carService;
            this.carService.CarsUpdated += OnCarsUpdated;

            FilteredCars = CollectionViewSource.GetDefaultView(carService.Cars);
            FilteredCars.Filter = Filtration;

            if (detectedPlateNumberService.DetectedPlateNumber != null)
                Filter = detectedPlateNumberService.DetectedPlateNumber;
        }

        private void OnCarsUpdated(object sender, CarsList e)
        {
            //FilteredCars.Refresh();
            RaisePropertyChanged(nameof(FilteredCars));            
        }

        private bool Filtration(object obj)
        {
            if (string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter))
                return true;
            var _car = (Car)obj;
            return _car.PlateNumberForward.ToUpper().Contains(Filter.ToUpper());
        }

        private void ApplyFilter()
        {
            FilteredCars.Refresh();

            //if (FilteredCars.Cast<object>().Count() == 1)
            //{
            //    var oneCar = FilteredCars.Cast<Car>().First();
            //    if (Filter != oneCar.PlateNumberForward)
            //        SelectedCar = oneCar;
            //}
            //else
                SelectedCar = null;

            RaisePropertyChanged(nameof(FilteredCars));
        }

        private void Open()
        {
            //OpenBarrier();

            var writedNumber = StringExtensions.TransliterateToRu(Filter);
            var plateNumber = selectedCar!=null?selectedCar.PlateNumberForward : writedNumber;

            logger.Warn($"КПП: Для машины {plateNumber} открыт шлагбаум на {areaService.SelectedArea.Name} по причине: {PassReasons[selectedReasonIndex]}. (Пользователь: {authService.AuthorizedUserLogin}).");

            SelectedCar = null;
        }

        private bool CanOpen()
        {
            if (RequiredReason && SelectedReasonIndex == 0) return false;

            if (selectedCar != null)
            {
                //var isAwaitingCar = selectedCar.CarState.TypeName == typeof(AwaitingState).Name;
                //var isExistingCar = selectedCar.CarState.TypeName == typeof(ExitPassGrantedState).Name;
                //var isInspection = selectedCar.IsInspectionRequired;
                //var inAreaCar = selectedCar.Area?.Id == areaService.SelectedArea.Id;

                //if (inAreaCar)
                //    if (isAwaitingCar || isExistingCar)
                //        if (!isInspection)
                return true;
            }
            else
            {
                if (SelectedReasonIndex != -1)
                    return true;
            }

            return false;
        }

        private void OpenBarrier()
        {
            using (var db = new WarehouseContext())
            {
                var barrier = db.BarrierInfos.FirstOrDefault(x => x.AreaId == areaService.SelectedArea.Id);
                if (barrier == null)
                    logger.Error($"Не удалось открыть шлагбаум на территории {areaService.SelectedArea.Name}. Шлагбаум не найден.");
                else
                    barrierService.Open(barrier);
            }
        }
    }
}
