using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingBackup
{
    public int Id { get; set; }

    public bool FlagPowerBackup { get; set; }

    public int IntervalBackup { get; set; }

    public string PathBackup { get; set; } = null!;

    public DateTime TimeBackup { get; set; }

    public string? RestorePath { get; set; }
}
