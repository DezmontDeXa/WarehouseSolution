using CheckPointControl.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Services;
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
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
            containerRegistry.RegisterForNavigation<SelectAreaView>();
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

        private void OnAreaSelected(object sender, SharedLibrary.DataBaseModels.Area e)
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