namespace Warehouse.Interfaces.Barriers
{
    public interface IBarrierInfo
    {
        int AreaId { get; }
        int Id { get; }
        string? Login { get; }
        string? Name { get; }
        string? Password { get; }
        string Uri { get; }
    }
}