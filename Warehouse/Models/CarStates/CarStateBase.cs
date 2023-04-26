using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CarStates
{
    public abstract class CarStateBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string TypeName { get; protected set; }

        public CarStateBase(int id, string name)
        {
            Id = id;
            Name = name;
            TypeName = GetType().Name;
        }

        public void AddThatStateToDB(WarehouseContext db)
        {
            var stateInDb = db.CarStates.FirstOrDefault(x => x.Id == Id && x.Name == Name && x.TypeName == TypeName);
            if (stateInDb == null)
                db.CarStates.Add(new CarState() { Id = Id, Name = Name, TypeName = TypeName });
        }
    }

    public class AwaitingState : CarStateBase
    {
        public AwaitingState() : base(0, "Ожидается") { }
    }
    public class OnEnterState : CarStateBase
    {
        public OnEnterState() : base(1, "На въезде") { }
    }
    public class AwaitingWeighingState : CarStateBase
    {
        public AwaitingWeighingState() : base(2, "Ожидает взвешивание") { }
    }
    public class WeighingState : CarStateBase
    {
        public WeighingState() : base(3, "Взвешивается") { }
    }
    public class LoadingState : CarStateBase
    {
        public LoadingState() : base(4, "Погрузка") { }
    }
    public class UnloadingState : CarStateBase
    {
        public UnloadingState() : base(5, "Разгрузка") { }
    }
    public class ExitingForChangeAreaState : CarStateBase
    {
        public ExitingForChangeAreaState() : base(6, "Выезжает для смены территории") { }
    }
    public class ChangingAreaState : CarStateBase
    {
        public ChangingAreaState() : base(7, "Едет к другой территории") { }
    }

    //public class RequiredInspectioState : CarStateBase
    //{
    //    public RequiredInspectioState() : base(0, "Требуется инспекция") { }
    //}
}
