using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsVideoSurveillance
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public string Name { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public int Port { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int WaitTime { get; set; }

    public int TryTimes { get; set; }

    public int StreamTypeMiniWindow { get; set; }

    public int StreamTypeFullWindow { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedByUserId { get; set; }

    public virtual ICollection<AuditWeightingPhoto> AuditWeightingPhotos { get; set; } = new List<AuditWeightingPhoto>();

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
