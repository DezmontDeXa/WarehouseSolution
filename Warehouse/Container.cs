using Ninject.Modules;
using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CameraRoles;
using Warehouse.Models.CameraRoles.Implements;
using Warehouse.Models.CarStates;
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
            BindCarStates();
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

        private void BindCarStates()
        {
            Bind<CarStateBase>().To<AwaitingState>();
            Bind<CarStateBase>().To<OnEnterState>();
            Bind<CarStateBase>().To<AwaitingWeighingState>();
            Bind<CarStateBase>().To<WeighingState>();
            Bind<CarStateBase>().To<LoadingState>();
            Bind<CarStateBase>().To<UnloadingState>();
            Bind<CarStateBase>().To<ExitingForChangeAreaState>();
            Bind<CarStateBase>().To<ChangingAreaState>();
            Bind<CarStateBase>().To<ExitPassGrantedState>();
            // TODO: Add other car states and run app for add to database
            Bind<CarStatesToDB>().ToSelf().InSingletonScope();
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
