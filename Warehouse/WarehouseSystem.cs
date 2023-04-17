using NLog;
using Warehouse.CameraRoles;
using Warehouse.DBModels;
using Warehouse.Models;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly WarehouseContext _db;
        private readonly List<CameraListener> _cameraListeners;
        private readonly List<CameraRoleBase> _cameraRoles;

        public WarehouseSystem(ILogger logger, WarehouseContext db, List<CameraRoleBase> cameraRoles)
        {
            _logger = logger;
            _db = db;
            _cameraRoles = cameraRoles;
            _cameraListeners = new List<CameraListener>();
        }

        public void Run()
        {
            RunCameras();
        }

        private void RunCameras()
        {
            foreach (var cameraEntity in _db.Cameras)
            {
                if (cameraEntity == null) continue;
                var listener = new CameraListener(cameraEntity);
                _cameraListeners.Add(listener);
                listener.OnNotification += Listener_OnNotification;
                listener.OnError += Listener_OnError;
            }
        }

        private void Listener_OnError(object? sender, Exception e)
        {
            var listener = (CameraListener)sender;
            _logger.Error($"Error while listening {listener.Camera.Name}. Listener will be restarted.");
        }

        private void Listener_OnNotification(object? sender, CameraNotifyBlock e)
        {
            if(TryGetCameraRole((CameraListener)sender, out var cameraRole))
                cameraRole.Execute(_db);
        }

        private bool TryGetCameraRole(CameraListener listener, out CameraRoleBase cameraRole)
        {
            cameraRole = null;

            var roleId = listener.Camera.RoleId;
            var cameraRoleEntity = _db.CameraRoles.FirstOrDefault(x => x.Id == roleId);
            if (cameraRoleEntity == null)
            {
                _logger.Error($"Failed to execute camera role action. Camera: {listener.Camera.Name}. CameraRole with id {roleId} not found.");
                return false;
            }

            cameraRole = _cameraRoles.FirstOrDefault(x => x.GetType().Name == cameraRoleEntity.TypeName);
            if (cameraRole == null)
            {
                _logger.Error($"Failed to execute camera role action. Camera: {listener.Camera.Name}. CameraRole with typename {cameraRoleEntity.TypeName} not registred in container.");
                return false;
            }
            return true;
        }
    }
}
