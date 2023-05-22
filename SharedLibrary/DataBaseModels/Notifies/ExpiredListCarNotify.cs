namespace SharedLibrary.DataBaseModels;

public partial class ExpiredListCarNotify : CarNotifyBase
{
    public string DetectedPlateNumber { get; set; }

    public string Direction { get; set; }

    public byte[] PlateNumberPicture { get; set; }

    public virtual Car? Car { get; set; }

    public virtual WaitingList? WaitingList { get; set; }

    public virtual Camera? Camera { get; set; }

    public virtual CameraRole? Role { get; set; }
}
