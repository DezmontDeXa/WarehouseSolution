namespace Warehouse.Interfaces.DataBase
{
    public interface ICameraRole
    {
        string? Description { get; set; }
        int Id { get; set; }
        string? Name { get; set; }
        string? TypeName { get; set; }
    }
}