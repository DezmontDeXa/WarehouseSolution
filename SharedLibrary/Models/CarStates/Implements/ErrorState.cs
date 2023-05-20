namespace Warehouse.Models.CarStates.Implements
{
    public class ErrorState : CarStateBase
    {
        public ErrorState() : base(10, "Ошибка") { }
    }

    //public class RequiredInspectioState : CarStateBase
    //{
    //    public RequiredInspectioState() : base(0, "Требуется инспекция") { }
    //}
}
