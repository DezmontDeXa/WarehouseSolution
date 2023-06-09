using NLog;
using SharedLibrary.DataBaseModels;
using Warehouse.Models.CameraRoles;
using CameraListenerService;
using NaisServiceLibrary;
using Microsoft.EntityFrameworkCore;
using WarehouseConfgisService.Models;
using WarehouseConfigService;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly List<CameraListener> _cameraListeners;
        private readonly List<CameraRoleBase> _cameraRoles;
        private readonly Dictionary<CameraListener, CameraRoleBase> _cameraRolesMap;
        private readonly Dictionary<CameraListener, Camera> _listenersToCameraMap;

        public WarehouseSystem(ILogger logger, List<CameraRoleBase> cameraRoles)
        {
            _logger = logger;
            _cameraRoles = cameraRoles;
            _cameraListeners = new List<CameraListener>();
            _cameraRolesMap = new Dictionary<CameraListener, CameraRoleBase>();
            _listenersToCameraMap = new Dictionary<CameraListener, Camera>();
        }

        public async void RunAsync()
        {
            await Task.Run(RunCameras);
#if DEBUG
            //var tests = new WarehouseSystemTests(_db, _cameraListeners);
            //tests.RunNormalPipelineTest();
#endif
        }

        private void RunCameras()
        {
            using (var configsDb = new WarehouseConfig())
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

        private CameraRoleBase GetCameraRole(int roleId)
        {
            using (var db = new WarehouseContext())
            {
                var role = db.CameraRoles.First(x => x.Id == roleId);
                return _cameraRoles.FirstOrDefault(x => x.GetType().Name == role.TypeName);
            }
        }

        private void Listener_OnError(object? sender, Exception e)
        {
            var listener = (CameraListener)sender;
            _logger.Error(e, $"Error while listening {_listenersToCameraMap[listener].Name}. {e.Message}. Listener will be restarted. Check stacktrace in file or db.");
        }

        private void Listener_OnNotification(object? sender, CameraNotifyBlock notifyBlock)
        {
            var listener = (CameraListener)sender;
            var listenerToRole = _cameraRolesMap.FirstOrDefault(x => x.Key.GetHashCode() == listener.GetHashCode());
            listenerToRole.Value.Execute(_listenersToCameraMap[listener], notifyBlock);
        }
    }
}
