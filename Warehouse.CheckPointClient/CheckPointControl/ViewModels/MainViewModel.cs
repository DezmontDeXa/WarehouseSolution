using CheckPointControl.Services;
using NLog;
using Prism.Mvvm;
using Prism.Regions;
using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using System.Linq;
using Warehouse.CheckPointClient.Services;
using Warehouse.Models.CarStates.Implements;
using static CheckPointControl.Services.CarsService;

namespace CheckPointControl.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public CarsList AwaitingCars => carsService.Cars.ByTargetArea(areaService.SelectedArea).ByState<AwaitingState>();
        public CarsList OnAreaCars => carsService.Cars.ByArea(areaService.SelectedArea);
        public CarsList OnLoadingCars => carsService.Cars.ByArea(areaService.SelectedArea).ByState<LoadingState, UnloadingState>();
        public CarsList ExitPassGrantedCars => carsService.Cars.ByArea(areaService.SelectedArea).ByState<ExitPassGrantedState, ExitingForChangeAreaState>();
        public Car SelectedCar { get => selectedCar; set => SetProperty(ref selectedCar, value); }

        private readonly AreaService areaService;
        private readonly CarsService carsService;
        private readonly AutorizationService authorizationService;
        private readonly ILogger logger;
        private Car selectedCar;

        public MainViewModel(AreaService areaService, AutorizationService authorizationService, CarsService carsService, ILogger logger)
        {
            this.areaService = areaService;
            this.authorizationService = authorizationService;
            this.carsService = carsService;
            this.logger = logger;

            carsService.CarsUpdated += OnCarsUpdated;
        }

        private void OnCarsUpdated(object sender, CarsList e)
        {
            RaisePropertyChanged(nameof(AwaitingCars));
            RaisePropertyChanged(nameof(OnAreaCars));
            RaisePropertyChanged(nameof(OnLoadingCars));
            RaisePropertyChanged(nameof(ExitPassGrantedCars));
        }
    }
}
