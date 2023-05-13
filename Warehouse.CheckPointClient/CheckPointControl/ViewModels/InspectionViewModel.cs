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
    public class InspectionViewModel : BindableBase
    {
        public Car SelectedCar { get => selectedCar; set { SetProperty(ref selectedCar, value); RaisePropertyChanged(nameof(HasSelectedCar)); } }
        public IEnumerable<Car> RequiredInspectionCars => carsService.Cars.ByArea(areaService.SelectedArea).WithInspectionRequired();
        public string PassReason { get => passReason; set => SetProperty(ref passReason, value); }
        public ICommand InspectionPassCommand => inspectionPassCommand ??= new ActionCommand(InspectionPass);
        public bool HasSelectedCar => SelectedCar != null;

        private Car selectedCar;
        private string passReason;
        private ActionCommand inspectionPassCommand;
        private readonly ILogger logger;
        private readonly AreaService areaService;
        private readonly AutorizationService authService;
        private readonly CarsService carsService;
        private readonly IBarriersService barrierService;

        public InspectionViewModel(ILogger logger, IBarriersService barriersService, AreaService areaService, AutorizationService authService, CarsService carsService)
        {
            this.logger = logger;
            this.areaService = areaService;
            this.authService = authService;
            this.carsService = carsService;
            this.barrierService = barriersService;

            carsService.CarsUpdated += OnCarsUpdated;
        }

        private void OnCarsUpdated(object sender, CarsService.CarsList e)
        {
            RaisePropertyChanged(nameof(RequiredInspectionCars));
        }

        private void InspectionPass()
        {
            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == selectedCar.Id);
                carInDb.IsInspectionRequired = false;
                db.SaveChanges();

                OpenBarrier(db);
            }

            logger.Warn($"Машина с номером {selectedCar.PlateNumberForward} была пропущена клиентом КПП (Пользоваатель: {authService.AuthorizedUserLogin}) по причине: {PassReason}.");
            PassReason = null;
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
