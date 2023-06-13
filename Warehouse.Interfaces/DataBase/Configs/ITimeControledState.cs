namespace Warehouse.Interfaces.DataBase.Configs
{
    public interface ITimeControledState
    {
        int? CarStateId { get; set; }
        int Id { get; set; }
        int Timeout { get; set; }
    }
}