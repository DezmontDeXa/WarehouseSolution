namespace Warehouse.Interfaces.DataBase
{
    public interface ICarStateTimer
    {
        int? CarId { get; set; }
        int? CarStateId { get; set; }
        int Id { get; set; }
        bool IsAlive { get; set; }
        long StartTimeTicks { get; set; }
        int? TimeControledStateId { get; set; }
    }
}