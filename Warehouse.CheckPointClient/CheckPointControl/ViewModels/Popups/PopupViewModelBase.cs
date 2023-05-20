using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse.CheckPointClient.Core;

namespace CheckPointControl.ViewModels.Popups
{
    public abstract class PopupViewModelBase : BindableBase
    {
        public ICommand ClosePopupCommand => closePopupCommand ??= new DelegateCommand(ClosePopup);

        private readonly IRegionManager regionManager;
        private DelegateCommand closePopupCommand;

        public PopupViewModelBase(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        private void ClosePopup()
        {
            var region = regionManager.Regions[RegionNames.PopupRegion];
            var thisView = region.Views.First(x => ((UserControl)x).DataContext == this);
            region.Remove(thisView);
        }
    }
}
