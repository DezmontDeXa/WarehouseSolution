namespace SharedLibrary.DataBaseModels;

public class CarDetectedNotify : CarNotifyBase
{
    public virtual Car? Car { get; set; }
    public int? CameraId { get; set; }
}
