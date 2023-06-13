namespace Warehouse.Interfaces.DataBase
{
    public interface ICarState
    {
        int Id { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
    }
}