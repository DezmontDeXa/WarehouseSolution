using NLog;
using Warehouse.CarStates;
using Warehouse.CarStates.Implements;
using Warehouse.DataBase.Models.Main;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.Processors.Car.Core
{
    public abstract class CarInfoProcessorBase : IProcessor<CarInfo>
    {
        protected ILogger Logger { get; }

        public CarInfoProcessorBase(ILogger logger)
        {
            Logger = logger;
        }

        public ProcessorResult Process(CarInfo info)
        {
            try
            {
                if (IsSuitableInfo(info))
                    return Action(info);
                else
                    return ProcessorResult.Next;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ProcessorResult.Finish;
            }
        }

        protected virtual bool IsSuitableInfo(CarInfo info)
        {
            return true;
        }

        protected abstract ProcessorResult Action(CarInfo info);

        protected void ChangeStatus(IWarehouseDataBaseMethods dbMethods, CarInfo info, CarStateBase state)
        {
            dbMethods.SetCarState(info.Car.Id, state.Id);
            Logger.Info(BuildLogMessage(info, $"Статус изменен c {info.State.Name} на {state.Name}"));
            info.State = BuildCarStateType(state);
        }

        protected ICarStateType BuildCarStateType(CarStateBase state)
        {
            return new CarStateType()
            {
                Id = state.Id,
                Name = state.Name,
                TypeName = state.TypeName
            };
        }

        protected string BuildLogMessage(CarInfo info, string msg)
        {
            return $"({info.Camera.Name})({info.NormalizedPlateNumber}): {msg}";
        }

        protected CarStateBase SelectLoadingUnloadingState(CarInfo info)
        {
            if (info.Purposes.Contains("Погрузка"))
                return new LoadingState();
            if (info.Purposes.Contains("Разгрузка"))
                return new UnloadingState();

            throw new Exception("Не удалось установить цель заезда на другую территорию.");
        }
    }
}