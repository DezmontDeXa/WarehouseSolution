using CheckPointControl.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using Prism.Commands;
using Prism.Regions;
using Services;
using SharedLibrary.DataBaseModels;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace CheckPointControl.ViewModels.Popups
{
    public class InspectionRequiredPopupViewModel : PopupViewModelBase
    {
        public ICommand InspectionFailedCommand => inspectionFailedCommand ??= new DelegateCommand(InspectionFailed);
        public ICommand InspectionPassGrantedCommand => inspectionPassGrantedCommand ??= new DelegateCommand(InspectionPassGranted);

        private DelegateCommand inspectionFailedCommand;
        private DelegateCommand inspectionPassGrantedCommand;
        private InspectionRequiredCarNotify _notify;
        private readonly IBarriersService barrierService;
        private readonly AreaService areaService;
        private readonly ILogger logger;

        public InspectionRequiredPopupViewModel(
            IRegionManager regionManager, 
            NotifiesViewShowerService notifiesViewShowerService, 
            IBarriersService barrierService,
            AreaService areaService,
            ILogger logger) : base(regionManager, notifiesViewShowerService)
        {
            _notify = notifiesViewShowerService.CurrentInspectionNotify;
            this.barrierService = barrierService;
            this.areaService = areaService;
            this.logger = logger;
        }

        private void InspectionFailed()
        {
            using (var db = new WarehouseContext())
            {
                var car = db.Cars.First(x => x.Id == _notify.Car.Id);
                car.IsInspectionRequired = true;
                OpenBarrier(db);
                logger.Warn($"КПП ({areaService.SelectedArea.Name}):\t Машина ({car.PlateNumberForward}) провалила досмотр.");
            }

            ClosePopup();
        }

        private void InspectionPassGranted()
        {
            using(var db= new WarehouseContext())
            {
                var car = db.Cars.First(x => x.Id == _notify.Car.Id);
                car.IsInspectionRequired = false;
                OpenBarrier(db);
                logger.Info($"КПП ({areaService.SelectedArea.Name}):\t Машина ({car.PlateNumberForward}) успешно прошла досмотр. Открыть шлагбаум.");
            }
            ClosePopup();
        }

        private void OpenBarrier(WarehouseContext db)
        {
            var barrier = db.BarrierInfos.FirstOrDefault(x => x.AreaId == areaService.SelectedArea.Id);
            if (barrier == null)
                logger.Error($"Не удалось открыть шлагбаум на территории {areaService.SelectedArea.Name}. Шлагбаум не найден.");
            else
                barrierService.Open(barrier);
        }
    }
}
