using CheckPointControl.Services;
using Prism.Regions;
using SharedLibrary.DataBaseModels;

namespace CheckPointControl.ViewModels.Popups
{
    public class UnknownCarPopupViewModel : PopupViewModelBase
    {
        public byte[] PlateNumberImage { get => plateNumberImage; set => SetProperty(ref plateNumberImage, value); }

        private byte[] plateNumberImage;
        private UnknownCarNotify _notify;

        public UnknownCarPopupViewModel(IRegionManager regionManager, NotifiesViewShowerService notifiesViewShowerService) : base(regionManager, notifiesViewShowerService)
        {
            _notify = notifiesViewShowerService.CurrentUnknownCarNotify;
            PlateNumberImage = _notify.PlateNumberPicture;
        }
    }
}
