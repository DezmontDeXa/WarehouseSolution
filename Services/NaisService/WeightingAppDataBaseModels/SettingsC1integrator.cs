using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsC1integrator
{
    public int Id { get; set; }

    public string Ip { get; set; } = null!;

    public int Port { get; set; }

    public bool Enable { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Server { get; set; }

    public string? Refr { get; set; }

    public string? PathToDb { get; set; }

    public string? PathToImage { get; set; }
}
