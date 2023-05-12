using Microsoft.EntityFrameworkCore;
using Microsoft.Xaml.Behaviors.Core;
using NLog;
using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Warehouse.CheckPointClient.Services;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;

namespace CheckPointControl.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public ObservableCollection<Car> AwaitingCars => GetCarsByState<AwaitingState>();
        public ObservableCollection<Car> OnAreaCars => GetCarsOnArea();
        public ObservableCollection<Car> OnLoadingCars => GetCarsByState<LoadingState, UnloadingState>();
        public ObservableCollection<Car> ExitPassGrantedCars => GetCarsByState<ExitPassGrantedState, ExitingForChangeAreaState>();
        public ObservableCollection<Car> RequiredInspectionCars => GetCarsInspection();
        public Car InspectionSelectedCar
        {
            get => inspectionSelectedCar; 
            set
            {
                SetProperty(ref inspectionSelectedCar, value);
                RaisePropertyChanged(nameof(InspectionHasSelectedCar));
            }
        }
        public bool InspectionHasSelectedCar => InspectionSelectedCar != null;
        public string InspectionPassReason { get => inspectionPassReason; set => SetProperty(ref inspectionPassReason, value); }
        public ICommand InspectionPassCommand => inspectionPassCommand ??= new ActionCommand(InspectionPass);


        private List<Car> _cars = new List<Car>();
        private readonly AreaService areaService;
        private readonly AutorizationService authorizationService;
        private Car inspectionSelectedCar;
        private string inspectionPassReason;
        private ActionCommand inspectionPassCommand;

        public MainViewModel(AreaService areaService, AutorizationService authorizationService)
        {
            Task.Run(UpdateLists);
            this.areaService = areaService;
            this.authorizationService = authorizationService;
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
        private ObservableCollection<Car> GetCarsByState<T1, T2>()
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.CarState.TypeName == typeof(T1).Name || x.CarState.TypeName == typeof(T2).Name));
        }

        private ObservableCollection<Car> GetCarsOnArea()
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.Area.Id == areaService.SelectedArea.Id));
        }

        private ObservableCollection<Car> GetCarsInspection()
        {
            return new ObservableCollection<Car>(_cars.Where(x => x.IsInspectionRequired));
        }

        private void InspectionPass()
        {
            LogManager.GetCurrentClassLogger().Warn($"Машина с номером {inspectionSelectedCar.PlateNumberForward} была пропущена клиентом КПП (Пользоваатель: {authorizationService.AuthorizedUserLogin}) по причине: {InspectionPassReason}.");
            InspectionPassReason = null;
            InspectionSelectedCar = null;
        }
    }
}
