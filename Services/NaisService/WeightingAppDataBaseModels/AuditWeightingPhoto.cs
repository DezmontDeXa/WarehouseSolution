using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class AuditWeightingPhoto
{
    public long Id { get; set; }

    public int IdAudit { get; set; }

    public string FileName { get; set; } = null!;

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int IdSettingsVideoSurveillance { get; set; }

    public virtual AuditWeighing IdAuditNavigation { get; set; } = null!;

    public virtual SettingsVideoSurveillance IdSettingsVideoSurveillanceNavigation { get; set; } = null!;
}
