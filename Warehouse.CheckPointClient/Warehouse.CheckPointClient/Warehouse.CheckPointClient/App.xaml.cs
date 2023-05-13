using CheckPointControl;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using Services;
using SharedLibrary.Logging;
using System.Windows;
using Warehouse.CheckPointClient.Services;
using Warehouse.CheckPointClient.Services.Interfaces;
using Warehouse.CheckPointClient.Views;
using Warehouse.Services;

namespace Warehouse.CheckPointClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            LoggingConfigurator.ConfigureLogger();
            containerRegistry.Register<ILogger>(LogManager.GetCurrentClassLogger);
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            // DummyBarrierService for development
            containerRegistry.RegisterSingleton<IBarriersService, DummyBarrierService>();
            containerRegistry.RegisterSingleton<AutorizationService>();
            containerRegistry.RegisterSingleton<AreaService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // disable for development
            //moduleCatalog.AddModule<AutorizationModule>();
            moduleCatalog.AddModule<CheckPointControlModule>();
        }
    }
}
