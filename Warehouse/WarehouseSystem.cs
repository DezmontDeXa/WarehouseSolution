using NLog;
using Warehouse.CameraListeners;
using Warehouse.ConfigDataBase.Context;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.CameraRoles;
using Warehouse.Interfaces.CamerasListener;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly List<ICameraListener> _cameraListeners;
        private readonly List<ICameraRoleBase> _cameraRoles;
        private readonly IAppSettings settings;
        private readonly IWarehouseDataBaseMethods dbMethods;
        private readonly Dictionary<ICameraListener, ICameraRoleBase> _cameraRolesMap;
        private readonly Dictionary<ICameraListener, ICamera> _listenersToCameraMap;

        public WarehouseSystem(ILogger logger, List<ICameraRoleBase> cameraRoles, IAppSettings settings, IWarehouseDataBaseMethods dbMethods)
        {
            _logger = logger;
            _cameraRoles = cameraRoles;
            this.settings = settings;
            this.dbMethods = dbMethods;
            _cameraListeners = new List<ICameraListener>();
            _cameraRolesMap = new Dictionary<ICameraListener, ICameraRoleBase>();
            _listenersToCameraMap = new Dictionary<ICameraListener, ICamera>();
        }

        public async void RunAsync()
        {
            await Task.Run(RunCameras);
        }

        private void RunCameras()
        {
            using (var configsDb = new WarehouseConfig(settings))
                foreach (var cameraEntity in configsDb.Cameras.ToList())
                {
                    if (cameraEntity == null) continue;
                    var listener = new CameraListener(new Uri(cameraEntity.Link));
                    _cameraListeners.Add(listener);
                    _cameraRolesMap.Add(listener, GetCameraRole(cameraEntity.RoleId));
                    _listenersToCameraMap.Add(listener, cameraEntity);
                    listener.OnNotification += Listener_OnNotification;
                    listener.OnError += Listener_OnError;
                }
        }

        private ICameraRoleBase GetCameraRole(int roleId)
        {
            var cameraRole = dbMethods.GetCameraRoleById(roleId);
            var roleBase = _cameraRoles.FirstOrDefault(x => x.GetType().Name == cameraRole.TypeName);
            if (roleBase == null)
                throw new NullReferenceException($"Cannot find cameraRole with id {roleId}");
            return roleBase;
        }

        private void Listener_OnError(object? sender, Exception e)
        {
            var listener = (ICameraListener)sender;
            _logger.Error(e, $"Error while listening {_listenersToCameraMap[listener].Name}. {e.Message}. Listener will be restarted. Check stacktrace in file or db.");
        }

        private void Listener_OnNotification(object? sender, ICameraNotifyBlock notifyBlock)
        {
            var listener = (ICameraListener)sender;
            var listenerToRole = _cameraRolesMap.FirstOrDefault(x => x.Key.GetHashCode() == listener.GetHashCode());
            listenerToRole.Value.Execute(_listenersToCameraMap[listener], notifyBlock);
        }
    }
}
