namespace Warehouse.Interfaces.CamerasListener
{
    public interface ICameraListener
    {
        event EventHandler<Exception>? OnError;
        event EventHandler<ICameraNotifyBlock>? OnNotification;

        void Dispose();
    }
}