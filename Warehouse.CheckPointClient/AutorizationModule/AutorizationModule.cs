using Autorization.Services;
using Autorization.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Warehouse.CheckPointClient.Core;

namespace Autorization
{
    public class AutorizationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AutorizationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.Register<AutorizationService>();
        }
    }
}