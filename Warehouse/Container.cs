using Ninject.Activation;
using Ninject.Modules;
using NLog;
using NLog.Fluent;
using NLog.Targets;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CameraRoles;
using Warehouse.Models.CameraRoles.Implements;
using Warehouse.Models.CarStates;
using Warehouse.Models.CarStates.Implements;
using Warehouse.Services;

namespace Warehouse
{
    public class Container : NinjectModule
    {
        public override void Load()
        {
            ConfigureLogger();
            Bind<ILogger>().ToMethod((context)=> LogManager.GetLogger(
                context.Request.Target.Member.DeclaringType.Name, 
                context.Request.Target.Member.DeclaringType));
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
            Bind<CameraRoleBase>().To<ExitRole>();
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
            Bind<CarStateBase>().To<FinishState>();
            // TODO: Add other car states and run app for add to database
            Bind<CarStatesToDB>().ToSelf().InSingletonScope();
        }

        private void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new FileTarget("logfile") { FileName = $"logs/{DateTime.Now.ToString("\\yyyy-\\MM-\\dd")}.log" };
            var logconsole = new ColoredConsoleTarget("logconsole");
            logconsole.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message:withexception=false}";

            var dbTarget = BuildDatabaseTarget();

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, dbTarget);

            // Apply config           
            LogManager.Configuration = config;

        }

        private DatabaseTarget BuildDatabaseTarget()
        {
            var target = new DatabaseTarget();

            target.DBProvider = "System.Data.SqlClient";
            target.ConnectionString = "Server=COMPUTER;Database=Warehouse;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=True;";
            target.CommandText = "INSERT INTO Logs(CreatedOn,Message,Level,Exception,StackTrace,Logger) VALUES (@datetime,@message,@level,@exception,@trace,@logger)";

            var param = new DatabaseParameterInfo();
            param.Name = "@datetime";
            param.Layout = "${date}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@message";
            param.Layout = "${message}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@level";
            param.Layout = "${level}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@exception";
            param.Layout = "${exception:format=type}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@trace";
            param.Layout = "${exception:format=stacktrace}";
            target.Parameters.Add(param);

            param = new DatabaseParameterInfo();
            param.Name = "@logger";
            param.Layout = "${logger}";
            target.Parameters.Add(param);

            return target;
        }
    }
}
