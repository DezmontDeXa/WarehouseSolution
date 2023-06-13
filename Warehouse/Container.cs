using Ninject.Activation;
using Ninject.Modules;
using NLog;
using Warehouse.AppSettings;
using Warehouse.Barriers;
using Warehouse.CameraRoles;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.DbMethods;
using Warehouse.FindCarServices;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.FindCarServices;
using Warehouse.Interfaces.RusificationServices;
using Warehouse.Interfaces.TimeControl;
using Warehouse.Interfaces.WaitingListServices;
using Warehouse.Logging;
using Warehouse.Nais;
using Warehouse.RusificationServices;
using Warehouse.TimeControl;
using Warehouse.WaitingListsServices;

namespace Warehouse
{
    public class Container : NinjectModule
    {
        public override void Load()
        {
            var appSettings = new DefaultAppSettings();
            appSettings.Load();
            LoggingConfigurator.ConfigureLogger(appSettings);

            Bind<IAppSettings>().ToConstant(appSettings).InSingletonScope();
            Bind<ILogger>().ToMethod(GetLoggerDelegate());

            BindCameraRoles();
            BindCarStates();

            Bind<NaisDataBaseConnection>().ToSelf().InSingletonScope();
            Bind<NaisRecordsObserver>().ToSelf().InSingletonScope();
            Bind<NaisService>().ToSelf().InSingletonScope();
            Bind<IRussificationService>().To<RussificationService>().InSingletonScope();
            Bind<IBarriersService>().To<SimpleBarrierService>().InSingletonScope();
            Bind<IFindCarService>().To<FuzzyFindCarService>().InSingletonScope();
            Bind<IWaitingListsService>().To<WaitingListsService>().InSingletonScope();
            Bind<ITimeControler>().To<TimeController>().InSingletonScope();
            Bind<IWarehouseDataBaseMethods>().To<WarehouseDataBaseMethods>().InSingletonScope();
            Bind<WarehouseSystem>().ToSelf().InSingletonScope();
        }

        private static Func<IContext, Logger> GetLoggerDelegate()
        {
            return (context) => LogManager.GetLogger(
                            context.Request.Target.Member.DeclaringType.Name,
                            context.Request.Target.Member.DeclaringType);
        }

        private void BindCameraRoles()
        {
            Bind<ICameraRoleBase>().To<BeforeEnterRole>();
            Bind<ICameraRoleBase>().To<AfterEnterRole>(); 
            Bind<ICameraRoleBase>().To<OnWeightingRole>();
            Bind<ICameraRoleBase>().To<ExitRole>();
            Bind<ICameraRoleBase>().To<EnterRole>();
            Bind<ICameraRoleBase>().To<ExitFromAnotherAreaRole>();
        }

        private void BindCarStates()
        {
            Bind<ICarStateBase>().To<AwaitingState>();
            Bind<ICarStateBase>().To<OnEnterState>();
            Bind<ICarStateBase>().To<AwaitingWeighingState>();
            Bind<ICarStateBase>().To<WeighingState>();
            Bind<ICarStateBase>().To<LoadingState>();
            Bind<ICarStateBase>().To<UnloadingState>();
            Bind<ICarStateBase>().To<SamplingState>();
            Bind<ICarStateBase>().To<ExitingForChangeAreaState>();
            Bind<ICarStateBase>().To<ChangingAreaState>();
            Bind<ICarStateBase>().To<ExitPassGrantedState>(); 
            Bind<ICarStateBase>().To<FinishState>();
            Bind<ICarStateBase>().To<ErrorState>();
        }

    }
}
