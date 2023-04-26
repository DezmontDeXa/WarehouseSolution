using SharedLibrary.DataBaseModels;

namespace Warehouse.Models.CarStates
{
    public abstract class CarStateBase
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string TypeName { get; protected set; }

        protected CarStateBase(int id, string name)
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

        public override bool Equals(object? obj)
        {
            if(obj is CarState)
            {
                var carState = (CarState)obj;
                return Id == carState.Id;
            }

            return base.Equals(obj);
        }

        public static bool Equals<T>(CarState carState) where T : CarStateBase
        {
            var type = typeof(T);
            return type.Name == carState.TypeName;
        }
    }

    //public class RequiredInspectioState : CarStateBase
    //{
    //    public RequiredInspectioState() : base(0, "Требуется инспекция") { }
    //}
}
