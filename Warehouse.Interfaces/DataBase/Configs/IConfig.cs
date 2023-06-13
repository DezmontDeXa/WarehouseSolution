namespace Warehouse.Interfaces.DataBase.Configs
{
    public interface IConfig
    {
        int Id { get; set; }
        string Key { get; set; }
        string? Value { get; set; }
    }
}