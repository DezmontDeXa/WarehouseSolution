namespace Warehouse.Interfaces.DataBase
{
    public interface IWaitingList
    {
        AccessGrantType AccessGrantType { get; }
        string? Camera { get;  }
        ICollection<ICar> Cars { get; }
        string? Customer { get; }
        DateTime? Date { get; }
        int Id { get; }
        string Name { get; }
        int Number { get; }
        string? PurposeOfArrival { get; }
        string? Route { get; }
        string? Ship { get; }
    }
}