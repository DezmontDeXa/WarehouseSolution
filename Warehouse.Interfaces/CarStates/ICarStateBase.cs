namespace Warehouse.Interfaces.CarStates
{
    public interface ICarStateBase
    {
        int Id { get; }
        string Name { get; }
        string TypeName { get; }

        bool Equals(object? obj);
    }
}