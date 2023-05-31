using CheckPointControl.Services;
using Prism.Regions;
using SharedLibrary.DataBaseModels;

namespace CheckPointControl.ViewModels.Popups
{
    public class UnknownCarPopupViewModel : PopupViewModelBase
    {
        private byte[] plateNumberImage;
        private UnknownCarNotify _notify;
        private string detectedPlateNumber;
        private readonly DetectedPlateNumberService detectedPlateNumberService;
        public byte[] PlateNumberImage { get => plateNumberImage; set => SetProperty(ref plateNumberImage, value); }

        public UnknownCarPopupViewModel(
            IRegionManager regionManager, 
            NotifiesViewShowerService notifiesViewShowerService, 
            DetectedPlateNumberService detectedPlateNumberService) : base(regionManager, notifiesViewShowerService)
        {
            _notify = notifiesViewShowerService.CurrentUnknownCarNotify;
            PlateNumberImage = _notify.PlateNumberPicture;
            this.detectedPlateNumberService = detectedPlateNumberService;
            this.detectedPlateNumberService.DetectedPlateNumber = _notify.DetectedPlateNumber;
        }

        protected override void ClosePopup()
        {
            base.ClosePopup();
            detectedPlateNumberService.DetectedPlateNumber = null;
        }
    }
}
