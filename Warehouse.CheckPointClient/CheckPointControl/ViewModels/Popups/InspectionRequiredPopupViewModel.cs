using Prism.Commands;
using Prism.Regions;
using System.Windows.Input;

namespace CheckPointControl.ViewModels.Popups
{
    public class InspectionRequiredPopupViewModel : PopupViewModelBase
    {
        public ICommand InspectionFailedCommand => inspectionFailedCommand ??= new DelegateCommand(InspectionFailed);
        public ICommand InspectionPassGrantedCommand => inspectionPassGrantedCommand ??= new DelegateCommand(InspectionPassGranted);

        private DelegateCommand inspectionFailedCommand;
        private DelegateCommand inspectionPassGrantedCommand;

        public InspectionRequiredPopupViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }

        private void InspectionFailed()
        {

        }

        private void InspectionPassGranted()
        {

        }
    }
}
