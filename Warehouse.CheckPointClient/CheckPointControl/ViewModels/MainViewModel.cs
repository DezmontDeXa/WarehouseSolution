using CheckPointControl.Services;
using NLog;
using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;
using Warehouse.CheckPointClient.Services;
using Warehouse.Models.CarStates.Implements;
using static CheckPointControl.Services.CarsService;

namespace CheckPointControl.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public List<Car> AwaitingCars => carsService.Cars.ByArea(areaService.SelectedArea).ByState<AwaitingState>();
        public List<Car> OnAreaCars => carsService.Cars.ByArea(areaService.SelectedArea);
        public List<Car> OnLoadingCars => carsService.Cars.ByArea(areaService.SelectedArea).ByState<LoadingState, UnloadingState>();
        public List<Car> ExitPassGrantedCars => carsService.Cars.ByArea(areaService.SelectedArea).ByState<ExitPassGrantedState, ExitingForChangeAreaState>();
        public List<Car> RequiredInspectionCars => carsService.Cars.ByArea(areaService.SelectedArea).WithInspectionRequired();
                        
        private readonly AreaService areaService;
        private readonly CarsService carsService;
        private readonly AutorizationService authorizationService;
        private readonly ILogger logger;

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
            RaisePropertyChanged(nameof(RequiredInspectionCars));
        }


    }
}
