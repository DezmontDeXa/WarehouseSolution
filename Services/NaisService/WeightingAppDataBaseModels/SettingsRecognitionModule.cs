using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsRecognitionModule
{
    public int Id { get; set; }

    public int IdDirWeightRoom { get; set; }

    public bool Enable { get; set; }

    public string WorkMode { get; set; } = null!;

    public int PlatformNumber { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedByUserId { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
