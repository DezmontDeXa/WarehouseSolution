using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.DataBase.Models.Config;

public partial class Config : IConfig
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string? Value { get; set; }
}
