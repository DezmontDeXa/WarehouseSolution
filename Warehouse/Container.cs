﻿using Ninject.Modules;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CameraRoles;
using Warehouse.Models.CameraRoles.Implements;
using Warehouse.Services;

namespace Warehouse
{
    public class Container : NinjectModule
    {
        public override void Load()
        {
            ConfigureLogger();
            Bind<ILogger>().ToConstant(LogManager.GetCurrentClassLogger()).InSingletonScope();
            Bind<WarehouseContext>().ToSelf().InSingletonScope();
            BindCameraRoles();
            Bind<BarrierService>().ToSelf().InSingletonScope();
            Bind<WaitingListsService>().ToSelf().InSingletonScope();
            Bind<TimeControlService>().ToSelf().InSingletonScope();
            Bind<WarehouseSystem>().ToSelf().InSingletonScope();
        }

        private void BindCameraRoles()
        {
            Bind<CameraRoleBase>().To<BeforeEnterRole>();
            Bind<CameraRoleBase>().To<AfterEnterRole>(); 
            Bind<CameraRoleBase>().To<OnWeightingRole>();
            // TODO: Add other camera roles and run app for add to database
            Bind<CameraRolesToDB>().ToSelf().InSingletonScope();
        }

        private void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = $"logs/{DateTime.Now.ToString("\\yyyy-\\MM-\\dd")}.log" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;

        }

    }
}
