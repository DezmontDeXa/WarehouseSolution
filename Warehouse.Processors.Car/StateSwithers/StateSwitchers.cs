using NLog;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;
using Warehouse.Processors.Implements;

namespace Warehouse.Processors.Car.StateSwithers
{
    /// <summary>
    /// Въезд, подтверждение, ожидание, взвешивание, разгрузка, выезд, подтверждение выезда
    /// </summary>
    public class MainStateSwitchers : ProcessorsPipeline<CarInfo>
    {
        public MainStateSwitchers(BeforeEnterSwitcher beforeEnterSwitcher, AfterEnterSwitcher afterEnterSwitcher, FirstWeightningSwitch firstWeightningSwitch,
            SecondWeightningSwitch secondWeightningSwitch, ExitAfterSecondWeightingSwitcher exitAfterSecondWeightingSwitcher,
            ExitingForChangeAreaSwitcher exitingForChangeAreaSwitcher, ChangingAreaSwitcher changingAreaSwitcher,
            ExitFromAnotherAreaSwitcher exitFromAnotherAreaSwitcher, ExitForFreeSwitcher exitForFreeSwitcher )
        {
            AddProcessor(beforeEnterSwitcher);
            AddProcessor(afterEnterSwitcher);
            AddProcessor(firstWeightningSwitch);
            AddProcessor(secondWeightningSwitch);
            AddProcessor(exitAfterSecondWeightingSwitcher);

            AddProcessor(exitingForChangeAreaSwitcher);
            AddProcessor(changingAreaSwitcher);
            AddProcessor(exitFromAnotherAreaSwitcher);
            AddProcessor(exitForFreeSwitcher);
        }
    }

    // AwaitingState/ChangingAreaState=>OnEnterState
    public class BeforeEnterSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public BeforeEnterSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new OnEnterState());
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new BeforeEnterRole().Id)
            {
                if (info.State.Id == new AwaitingState().Id)
                    return true;

                if (info.State.Id == new ChangingAreaState().Id)
                    return true;
            }

            return false;
        }
    }

    // OnEnterState => ExitPassGrantedState/AwaitingFirstWeighingState/AwaitingSecondWeighingState
    public class AfterEnterSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public AfterEnterSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (!info.HasFirstWeightning)
            {
                switch (info.AccessType)
                {
                    case AccessType.Free:
                        ChangeStatus(dbMethods, info, new ExitPassGrantedState());
                        break;
                    case AccessType.Tracked:
                        ChangeStatus(dbMethods, info, new AwaitingFirstWeighingState());
                        break;
                }
                return ProcessorResult.Finish;
            }
            else if (!info.HasSecondWeightning)
            {
                ChangeStatus(dbMethods, info, new AwaitingSecondWeighingState());
                return ProcessorResult.Finish;
            }
            return ProcessorResult.Next;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new AfterEnterRole().Id)
                if (info.State.Id == new OnEnterState().Id)
                    return true;

            return false;
        }
    }

    // AwaitingFirstWeighingState/ExitPassGrantedState => WeighingState
    public class FirstWeightningSwitch : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public FirstWeightningSwitch(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new WeighingState());
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new OnWeightingRole().Id)
            {
                if (!info.HasFirstWeightning)
                {
                    if (info.AccessType == AccessType.Free)
                        if (info.State.Id == new AwaitingFirstWeighingState().Id)
                            return true;

                    if (info.AccessType == AccessType.Tracked)
                        if (info.State.Id == new ExitPassGrantedState().Id)
                            return true;
                }
            }

            return false;
        }
    }

    // после отработает наис и сменит статус на Loading/Unloading или ExitingForChangeArea

    #region if Loading/Unloading

    // Loading/Unloading/AwaitingSecondWeighingState -> WeighingState
    public class SecondWeightningSwitch : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public SecondWeightningSwitch(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new WeighingState());
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new OnWeightingRole().Id)
            {
                if (info.HasFirstWeightning)
                {
                    if (!info.HasSecondWeightning)
                    {
                        if (info.State.Id == new LoadingState().Id)
                            return true;
                        if (info.State.Id == new UnloadingState().Id)
                            return true;
                        if (info.State.Id == new AwaitingSecondWeighingState().Id)
                            return true;
                    }
                }
            }

            return false;
        }
    }

    // ExitingState => AwaitingState
    public class ExitAfterSecondWeightingSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public ExitAfterSecondWeightingSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            // TODO: Сколько живет список? Если машина в списке она может посещать территорию сколько угодно раз или один раз за один список?
            // При получении списков - отсечь просроченные
            // Если сколько угодно - то оставляем как есть.
            // Если только один раз - то для трекедных машина - статус Finish
            ChangeStatus(dbMethods, info, new AwaitingState());
            return ProcessorResult.Next;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new ExitRole().Id)
                if (info.State.Id == new ExitingState().Id)
                    return true;

            return false;
        }
    }

    #endregion

    #region if ExitingForChangeArea

    // ExitingForChangeArea => ChangingAreaState
    public class ExitingForChangeAreaSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public ExitingForChangeAreaSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new ChangingAreaState());
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new ExitRole().Id)
                if (info.State.Id == new ExitingForChangeAreaState().Id)
                    return true;

            return false;
        }
    }

    // ChangingAreaState => Loading/Unloading
    public class ChangingAreaSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public ChangingAreaSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            var state = SelectLoadingUnloadingState(info);
            ChangeStatus(dbMethods, info, state);
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new EnterRole().Id)
                if (info.State.Id == new ChangingAreaState().Id)
                    return true;

            return false;
        }
    }

    // Loading/Unloading => ChangingAreaState
    public class ExitFromAnotherAreaSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public ExitFromAnotherAreaSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new ChangingAreaState());
            return ProcessorResult.Finish;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new ExitFromAnotherAreaRole().Id)
            {
                if (info.State.Id == new LoadingState().Id)
                    return true;
                if (info.State.Id == new UnloadingState().Id)
                    return true;
            }

            return false;
        }
    }

    // BeforeEnterSwitcher

    // AfterEnterSwitcher

    // SecondWeightningSwitch

    // ExitAfterSecondWeightingSwitcher

    #endregion

    public class ExitForFreeSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public ExitForFreeSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            ChangeStatus(dbMethods, info, new AwaitingState());
            return ProcessorResult.Next;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new ExitRole().Id)
                if (info.State.Id == new ExitPassGrantedState().Id)
                    return true;

            return false;
        }
    }
}
