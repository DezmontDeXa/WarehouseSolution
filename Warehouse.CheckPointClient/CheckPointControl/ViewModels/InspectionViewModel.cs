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
        public Car InspectionSelectedCar { get => inspectionSelectedCar; set => SetProperty(ref inspectionSelectedCar, value); }
        public IEnumerable<Car> RequiredInspectionCars { get => requiredInspectionCars; set => SetProperty(ref requiredInspectionCars, value); }
        public string InspectionPassReason { get => inspectionPassReason; set => SetProperty(ref inspectionPassReason, value); }
        public ICommand InspectionPassCommand => inspectionPassCommand ??= new ActionCommand(InspectionPass);
        public bool InspectionHasSelectedCar => InspectionSelectedCar != null;


        private Car inspectionSelectedCar;
        private IEnumerable<Car> requiredInspectionCars;
        private string inspectionPassReason;
        private ActionCommand inspectionPassCommand;
        private readonly ILogger logger;
        private readonly AreaService areaService;
        private readonly AutorizationService authService;
        private readonly IBarriersService barrierService;

        public InspectionViewModel(ILogger logger, IBarriersService barriersService, AreaService areaService, AutorizationService authService)
        {
            this.logger = logger;
            this.areaService = areaService;
            this.authService = authService;
            this.barrierService = barriersService;
        }

        private void InspectionPass()
        {
            using (var db = new WarehouseContext())
            {
                var carInDb = db.Cars.First(x => x.Id == inspectionSelectedCar.Id);
                carInDb.IsInspectionRequired = false;

                var barrier = db.BarrierInfos.First(x => x.Area.Id == areaService.SelectedArea.Id);
                barrierService.Switch(barrier, SimpleBarrierService.BarrierCommand.Open);
                db.SaveChanges();
            }

            InspectionPassReason = null;
            InspectionSelectedCar = null;
            logger.Warn($"Машина с номером {inspectionSelectedCar.PlateNumberForward} была пропущена клиентом КПП (Пользоваатель: {authService.AuthorizedUserLogin}) по причине: {InspectionPassReason}.");
        }
    }
}
