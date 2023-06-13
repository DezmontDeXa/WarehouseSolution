namespace Warehouse.DataBase.Notifies;

public partial class InspectionRequiredCarNotify : CarNotifyBase
{
    public virtual Car? Car { get; set; }
}
