using Warehouse.Interfaces.CarStates;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.CarStates
{
    public abstract class CarStateBase : ICarStateBase
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

        public override bool Equals(object? obj)
        {
            if (obj is ICarState)
            {
                var carState = (ICarState)obj;
                return Id == carState.Id;
            }

            return base.Equals(obj);
        }

        public static bool Equals<T>(ICarState carState) where T : CarStateBase
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
