﻿using CheckPointControl.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Warehouse.CheckPointClient.Core;

namespace CheckPointControl
{
    public class CheckPointControlModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public CheckPointControlModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}