namespace SharedLibrary.DataBaseModels;

public partial class NotInListCarNotify : CarNotifyBase
{
    public string DetectedPlateNumber { get; set; }

    public string Direction { get; set; }

    public byte[] PlateNumberPicture { get; set; }

    public virtual Car? Car { get; set; }

    public int? CameraId { get; set; }

    public virtual CameraRole? Role { get; set; }
}
