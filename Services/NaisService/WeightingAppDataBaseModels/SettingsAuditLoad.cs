using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsAuditLoad
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public float LimitOfStart { get; set; }

    public string? PhotoDirectory { get; set; }

    public int MinLocation { get; set; }

    public int MaxLocation { get; set; }

    public float HistoricalTarasDelta { get; set; }

    public int PhotoPeriodSec { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
