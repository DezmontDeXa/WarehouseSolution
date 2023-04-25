using NLog;
using Warehouse.Services;
using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CameraRoles
{
    public abstract class CameraRoleBase
    {
        public string RoleName { get; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        protected ILogger Logger { get; private set; }
        private readonly WaitingListsService _waitingListsService;

        public CameraRoleBase(ILogger logger, WaitingListsService waitingListsService)
        {
            RoleName = GetType().Name;
            Logger = logger;
            _waitingListsService = waitingListsService;
        }

        public void AddThatRoleToDB(WarehouseContext db)
        {
            if (!db.CameraRoles.Any(x => x.TypeName == RoleName))
                db.CameraRoles.Add(new CameraRole() { Name = Name, Description = Description, TypeName = RoleName });
        }

        public void Execute(Camera camera, CameraNotifyBlock notifyBlock)
        {
            try
            {
                // Skip all events,except Car detected
                if (notifyBlock.EventType != "ANPR")
                    return;

                var plateNumber = GetPlateNumber(notifyBlock);
                Logger.Info($"{camera.Name}: Обнаружена машина ({plateNumber})");

                var direction = GetDirection(notifyBlock);
                if (camera.Direction != MoveDirection.Both && direction.ToLower() != camera.Direction.ToString().ToLower())
                {
                    Logger.Warn($"{camera.Name}: Направление: {direction}. Ожидалось \"{camera.Direction}\". Без действий.");
                    return;
                }

                var carAccessInfo = _waitingListsService.GetAccessTypeInfo(plateNumber);

                OnExecute(camera, notifyBlock, carAccessInfo, plateNumber, direction);
            }
            catch (Exception ex)
            {
                Logger?.Error($"Camera role execute failed. Camera: {camera.Name}. Role: {RoleName}. Ex: {ex}");
                return;
            }
        }

        protected string GetPlateNumber(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["licensePlate"]?.InnerText;
        }

        protected string GetDirection(CameraNotifyBlock block)
        {
            return block.XmlDocumentRoot["ANPR"]["direction"]?.InnerText;
            //return block.XmlDocument.SelectSingleNode("//EventNotificationAlert/ANPR/direction")?.InnerText;
        }

        protected abstract void OnExecute(Camera camera, CameraNotifyBlock notifyBlock, CarAccessInfo carAccessInfo, string plateNumber, string direction);
    }
}
