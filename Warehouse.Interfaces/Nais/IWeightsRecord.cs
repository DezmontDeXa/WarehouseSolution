namespace Warehouse.Interfaces.Nais
{
    public interface IWeightsRecord
    {
        float? FirstWeighting { get; set; }
        int Id { get; init; }
        float? Netto { get; set; }
        string? PlateNumber { get; init; }
        float? SecondWeighting { get; set; }
        string? StorageName { get; set; }
    }
}