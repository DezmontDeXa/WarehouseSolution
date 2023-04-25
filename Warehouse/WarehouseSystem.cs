using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CameraRoles;
using Warehouse.Tests;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly WarehouseContext _db;
        private readonly List<CameraListenerService> _cameraListeners;
        private readonly List<CameraRoleBase> _cameraRoles;
        private readonly Dictionary<CameraListenerService, CameraRoleBase> _cameraRolesMap;

        public WarehouseSystem(ILogger logger, WarehouseContext db, List<CameraRoleBase> cameraRoles)
        {
            _logger = logger;
            _db = db;
            _cameraRoles = cameraRoles;
            _cameraListeners = new List<CameraListenerService>();
            _cameraRolesMap = new Dictionary<CameraListenerService, CameraRoleBase>();
        }

        public void Run()
        {
            RunCameras();

#if DEBUG
            var tests = new WarehouseSystemTests(_db, _cameraListeners);
            tests.RunNormalPipelineTest();
#endif
        }

        private void RunCameras()
        {
            foreach (var cameraEntity in _db.Cameras)
            {
                if (cameraEntity == null) continue;
                var listener = new CameraListenerService(cameraEntity);
                _cameraListeners.Add(listener);
                _cameraRolesMap.Add(listener, GetCameraRole(cameraEntity.CameraRole));
                listener.OnNotification += Listener_OnNotification;
                listener.OnError += Listener_OnError;

            }
        }

        private CameraRoleBase GetCameraRole(CameraRole role)
        {
            return _cameraRoles.FirstOrDefault(x => x.GetType().Name == role.TypeName);
        }

        private void Listener_OnError(object? sender, Exception e)
        {
            var listener = (CameraListenerService)sender;
            _logger.Error($"Error while listening {listener.Camera.Name}. Exception: {e}. Listener will be restarted.");
        }

        private void Listener_OnNotification(object? sender, CameraNotifyBlock notifyBlock)
        {
            var listener = (CameraListenerService)sender;
            var listenerToRole = _cameraRolesMap.FirstOrDefault(x => x.Key.GetHashCode() == listener.GetHashCode());
            listenerToRole.Value.Execute(listener.Camera, notifyBlock);
        }
    }
}
