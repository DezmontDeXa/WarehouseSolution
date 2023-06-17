using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.CameraRoles.Implements;
using Warehouse.CarStates.Implements;
using Warehouse.Interfaces.DataBase;
using Warehouse.Processors.Car.Core;

namespace Warehouse.Processors.Car.StateSwithers.AAA
{
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
                if (info.State.Id == new AwaitingState().Id)
                    return true;

            return false;
        }
    }

    public class AfterEnterSwitcher : CarInfoProcessorBase
    {
        private readonly IWarehouseDataBaseMethods dbMethods;

        public AfterEnterSwitcher(ILogger logger, IWarehouseDataBaseMethods dbMethods) : base(logger)
        {
            this.dbMethods = dbMethods;
        }

        protected override ProcessorResult Action(CarInfo info)
        {
            if (info.State.Id == new OnEnterState().Id)
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
            return ProcessorResult.Next;
        }

        protected override bool IsSuitableInfo(CarInfo info)
        {
            if (info.Camera.RoleId == new AfterEnterRole().Id)
                return true;

            return false;
        }
    }

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
                    }
                }
            }

            return false;
        }
    }

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

}
