using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase.Configs;
using Warehouse.Interfaces.WaitingListServices;

namespace Warehouse.Interfaces.DataBase
{
    public interface IWarehouseDataBaseMethods
    {
        void CreateTimer(ICar car, ITimeControledState controlledState);
        ICameraRole GetCameraRoleById(int roleId);
        ICar? GetCarById(int id);
        ICar? GetCarByPlateNumber(string plateNumber);
        IEnumerable<ICar> GetCars();
        ICarState GetCarState(ICar car);
        IEnumerable<ICar> GetCarsWithWaitingLists();
        ICarState? GetStateById(int stateId);
        IEnumerable<ICarStateTimer> GetTimers();
        ICarState GetTimerTargetState(ITimeControledState controlledState);

        void RegisterCameraRoles(IEnumerable<ICameraRoleBase> cameraRoles);
        void RegisterCarStates(IEnumerable<ICarStateBase> carStates);
        void SendCarDetectedNotify(ICamera camera, ICarAccessInfo carAccessInfo);
        void SendExpriredListCarNotify(ICamera camera, ICar car, IWaitingList list, ICameraNotifyBlock pictureBlock, string plateNumber, string direction);
        void SendInspectionRequiredCarNotify(ICar car);
        void SendNotify<T>(T notify) where T : class;
        void SendNotInListCarNotify(ICamera camera, ICar car, ICameraNotifyBlock pictureBlock, string plateNumber, string direction);
        void SendUnknownCarNotify(ICamera camera, ICameraNotifyBlock pictureBlock, string plateNumber, string direction);
        void SetCarArea(ICar? car, int? areaId);
        void SetCarInspectionRequired(ICar car, bool value);
        void SetCarState(ICar car, ICarStateBase loadingState);
        void SetCarState(int car, int state);
        void SetCarState(ICar car, ICarState state);
        void SetCarStorage(ICar car, IStorage storage);
        void SetCarTargetArea(ICar car, int? areaId);
        void SetTimerIsAlive(ICarStateTimer timer, bool value);
    }
}