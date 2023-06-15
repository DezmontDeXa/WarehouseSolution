namespace Warehouse.DataBase.Models.Main.Notifies;

public partial class UnknownCarNotify : CarNotifyBase
{
    public string DetectedPlateNumber { get; set; }

    public string Direction { get; set; }

    public byte[] PlateNumberPicture { get; set; }

    public int? CameraId { get; set; }

    public virtual CameraRole? Role { get; set; }
}
