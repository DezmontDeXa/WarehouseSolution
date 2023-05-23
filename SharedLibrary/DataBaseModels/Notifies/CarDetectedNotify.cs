namespace SharedLibrary.DataBaseModels;

public class CarDetectedNotify : CarNotifyBase
{
    public virtual Car? Car { get; set; }
    public virtual Camera? Camera { get; set; }
}
