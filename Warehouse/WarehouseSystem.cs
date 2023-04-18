﻿using NLog;
using Warehouse.Services;
using Warehouse.DataBaseModels;
using Warehouse.Models.CameraRoles;

namespace Warehouse
{
    public class WarehouseSystem
    {
        private readonly ILogger _logger;
        private readonly WarehouseContext _db;
        private readonly List<CameraListenerService> _cameraListeners;
        private readonly List<CameraRoleBase> _cameraRoles;

        public WarehouseSystem(ILogger logger, WarehouseContext db, List<CameraRoleBase> cameraRoles)
        {
            _logger = logger;
            _db = db;
            _cameraRoles = cameraRoles;
            _cameraListeners = new List<CameraListenerService>();
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
                var listener = new CameraListenerService(cameraEntity);
                _cameraListeners.Add(listener);
                listener.OnNotification += Listener_OnNotification;
                listener.OnError += Listener_OnError;
            }
        }

        private void Listener_OnError(object? sender, Exception e)
        {
            var listener = (CameraListenerService)sender;
            _logger.Error($"Error while listening {listener.Camera.Name}. Listener will be restarted.");
        }

        private void Listener_OnNotification(object? sender, CameraNotifyBlock notifyBlock)
        {
            var listener = (CameraListenerService)sender; 
            if (TryGetCameraRole(listener, out var cameraRole))
                cameraRole.Execute(listener.Camera, notifyBlock);
        }

        private bool TryGetCameraRole(CameraListenerService listener, out CameraRoleBase cameraRole)
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
