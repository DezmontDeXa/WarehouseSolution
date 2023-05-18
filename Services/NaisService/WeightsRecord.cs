namespace NaisService
{
    public record WeightsRecord
    {
        public float? FirstWeighting { get; init; }
        public float? SecondWeighting { get; init; }
        public float? Netto { get; init; }
        public string? StorageName { get; init; }
        public string? PlateNumber { get; init; }
    }
}
