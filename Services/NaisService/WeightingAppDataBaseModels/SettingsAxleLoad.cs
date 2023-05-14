using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsAxleLoad
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedByUserId { get; set; }

    public int Interval { get; set; }

    public double LimitOfStart { get; set; }

    public int Window { get; set; }

    public int PeriodDelay { get; set; }

    public bool StoreData { get; set; }

    public float InterAxle { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
