namespace Warehouse.Models.CarStates.Implements
{
    public class AwaitingWeighingState : CarStateBase
    {
        public AwaitingWeighingState() : base(2, "Ожидает взвешивание") { }
    }

    //public class RequiredInspectioState : CarStateBase
    //{
    //    public RequiredInspectioState() : base(0, "Требуется инспекция") { }
    //}
}
