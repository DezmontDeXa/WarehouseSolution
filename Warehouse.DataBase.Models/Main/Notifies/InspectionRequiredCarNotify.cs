namespace Warehouse.DataBase.Models.Main.Notifies;

public partial class InspectionRequiredCarNotify : CarNotifyBase
{
    public virtual Car? Car { get; set; }
}
