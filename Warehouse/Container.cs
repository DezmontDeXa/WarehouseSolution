using Ninject.Modules;
using NLog;
using Warehouse.CameraRoles;
using Warehouse.Initializers;
using Warehouse.Models;

namespace Warehouse
{
    public class Container : NinjectModule
    {
        public override void Load()
        {
            InitLog();

            Bind<WarehouseContext>().ToSelf().InSingletonScope();
            Bind<ILogger>().ToConstant(LogManager.GetCurrentClassLogger());
            BindCameraRoles();
            Bind<CameraRolesInitializer>().ToSelf();
            Bind<WarehouseSystem>().ToSelf();
        }

        private void BindCameraRoles()
        {
            Bind<CameraRoleBase>().To<BeforeEnterRole>();
            Bind<CameraRoleBase>().To<AfterEnterRole>();
        }

        private void InitLog()
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
