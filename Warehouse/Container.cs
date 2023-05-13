using Ninject.Activation;
using Ninject.Modules;
using NLog;
using SharedLibrary.DataBaseModels;
using SharedLibrary.Logging;
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
            LoggingConfigurator.ConfigureLogger();
            Bind<ILogger>().ToMethod(GetLoggerDelegate());
            Bind<WarehouseContext>().ToSelf().InSingletonScope();
            BindCameraRoles();
            BindCarStates();
            Bind<SimpleBarrierService>().ToSelf().InSingletonScope();
            Bind<FuzzyFindCarService>().ToSelf().InSingletonScope();
            Bind<WaitingListsService>().ToSelf().InSingletonScope();
            Bind<TimeControlService>().ToSelf().InSingletonScope();
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

    }
}
