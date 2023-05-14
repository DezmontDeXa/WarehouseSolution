using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsServerReport
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public string? PhotoDirectory { get; set; }

    public string? Ipaddress { get; set; }

    public int? Port { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedByUserId { get; set; }

    public string WeighingName { get; set; } = null!;

    public string? ConnectionString { get; set; }

    public string? AuditPhotoDirectory { get; set; }
}
