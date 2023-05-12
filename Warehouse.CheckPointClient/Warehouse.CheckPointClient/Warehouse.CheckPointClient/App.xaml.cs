using Autorization;
using Microsoft.EntityFrameworkCore;
using NLog;
using Prism.Ioc;
using Prism.Modularity;
using SharedLibrary.Logging;
using System;
using System.Windows;
using Warehouse.CheckPointClient.Modules.ModuleName;
using Warehouse.CheckPointClient.Services;
using Warehouse.CheckPointClient.Services.Interfaces;
using Warehouse.CheckPointClient.Views;

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
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AutorizationModule>();
        }
    }
}
