namespace Warehouse.Interfaces.DataBase
{
    public interface ICar
    {
        int? AreaId { get; }
        int? CarStateId { get; }
        string? Driver { get; }
        bool FirstWeighingCompleted { get; }
        int Id { get; }
        bool IsInspectionRequired { get; }
        string PlateNumberBackward { get; }
        string PlateNumberForward { get; }
        string? PlateNumberSimilars { get; }
        bool SecondWeighingCompleted { get; }
        int? StorageId { get; }
        int? TargetAreaId { get; }
        ICollection<IWaitingList> WaitingLists { get; }
    }
}