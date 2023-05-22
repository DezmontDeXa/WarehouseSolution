using CheckPointControl.Services;
using CheckPointControl.Views;
using CheckPointControl.Views.Popups;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Services;
using SharedLibrary.DataBaseModels;
using System;
using Warehouse.CheckPointClient.Core;
using Warehouse.CheckPointClient.Services;

namespace CheckPointControl
{
    public class CheckPointControlModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly AutorizationService authService;
        private readonly AreaService areaService;
        private readonly CarNotifierService carNotifierService;

        public CheckPointControlModule(IRegionManager regionManager, AutorizationService authService, AreaService areaService)
        {
            _regionManager = regionManager;
            this.authService = authService;
            this.areaService = areaService;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            if (authService.RequiredAutorize)
                authService.Authorized += OnAuthorized;
            else
            {
                // For development only
                areaService.SelectedArea = areaService.Areas[0];
                ShowMainView();
            }
            containerProvider.Resolve<NotifiesViewShowerService>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<CarsService>();
            containerRegistry.RegisterSingleton<NotifiesQueueService>();
            containerRegistry.RegisterSingleton<NotifiesViewShowerService>();
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<SelectAreaView>();
            containerRegistry.RegisterForNavigation<UnknownCarPopup>();
            containerRegistry.RegisterForNavigation<InspectionRequiredPopup>();
        }

        private void OnAuthorized(object sender, EventArgs e)
        {
            authService.Authorized -= OnAuthorized;
            ShowAreaSelector();
        }

        private void ShowAreaSelector()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(SelectAreaView));
            areaService.AreaSelected += OnAreaSelected;
        }

        private void OnAreaSelected(object sender, Area e)
        {
            areaService.AreaSelected -= OnAreaSelected;
            ShowMainView();
        }

        private void ShowMainView()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(MainView));
        }
    }
}