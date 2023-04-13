using Warehouse.Models.Commands;

namespace Warehouse.Models.ControlServices
{
    public class Camera : ControlService
    {
        public int Id { get; }
        public string IP { get; }
        public string Login { get; }
        public string Password { get; }

        public event EventHandler<CameraDetectionEventArgs> Detected;
        public CarStatus ChangeStatusTo { get; }
        public KPPBarrier? Barrier { get; }

        private readonly ChangeStatusCommand _changeStatusCommand;
        private readonly OpenBarrierCommand _openBarierCommand;

        public Camera(ChangeStatusCommand changeStatusCommand, OpenBarrierCommand openBarierCommand)
        {
            _changeStatusCommand = changeStatusCommand;
            _openBarierCommand = openBarierCommand;
            Task.Run(Process);
        }

        private void Process()
        {
            while (true)
            {
                try
                {
                    //TODO: Чтение потока камеры
                    //TODO: Пораждение события Detected
                    string plateNumber = "";
                    // Вызвать при событии
                    ExecuteCommands(plateNumber);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void ExecuteCommands(string plateNumber)
        {
            _changeStatusCommand.Execute(this, plateNumber);
            _openBarierCommand.Execute(this, plateNumber);
        }
    }

    public class CameraDetectionEventArgs : EventArgs
    {
        public string PlateNumber { get; }
        public DateTime DateTime { get; }
        public MoveDirection Direction { get; }
    }

    public enum MoveDirection
    {
        ToCamera,
        FromCamera,
    }
}
