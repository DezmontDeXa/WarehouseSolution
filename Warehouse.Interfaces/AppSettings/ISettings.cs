namespace Warehouse.Interfaces.AppSettings
{
    public interface IAppSettings
    {
        string? ConfigConnectionString { get; }
        string? ConnectionString { get; }
        string? NaisConnectionString { get; }

        void Load();
    }
}