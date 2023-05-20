using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CarStates.Implements
{
    public class ExitingForChangeAreaState : CarStateBase
    {
        public ExitingForChangeAreaState() : base(6, "Выезжает для смены территории") { }

        public static string CreateContext(Area area)
        {
            return area.Id.ToString();
        }

        public static Area ParseContext(string context)
        {
            using(var db = new WarehouseContext())
            {
                return db.Areas.First(x=>x.Id == int.Parse(context));
            }
        }
    }

    //public class RequiredInspectioState : CarStateBase
    //{
    //    public RequiredInspectioState() : base(0, "Требуется инспекция") { }
    //}
}
