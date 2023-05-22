namespace SharedLibrary.DataBaseModels;

public partial class InspectionRequiredCarNotify : CarNotifyBase
{
    public virtual Car? Car { get; set; }
}
