using Prism.Regions;

namespace CheckPointControl.ViewModels.Popups
{
    public class UnknownCarPopupViewModel : PopupViewModelBase
    {
        public string PlateNumberImage { get => plateNumberImage; set => SetProperty(ref plateNumberImage, value); }

        private string plateNumberImage;

        public UnknownCarPopupViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
    }
}
