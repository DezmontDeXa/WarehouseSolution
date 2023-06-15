namespace Warehouse.DataBase.Models.Main.Notifies;

public class CarDetectedNotify : CarNotifyBase
{
    // заменить на ID машины
    public virtual Car? Car { get; set; }
    public int? CameraId { get; set; }
}
