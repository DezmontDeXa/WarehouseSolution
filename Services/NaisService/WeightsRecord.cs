namespace NaisServiceLibrary
{
    public record WeightsRecord
    {
        public int Id { get; init; }
        public float? FirstWeighting { get; set; }
        public float? SecondWeighting { get; set; }
        public float? Netto { get; set; }
        public string? StorageName { get; set; }
        public string? PlateNumber { get; init; }
    }
}
