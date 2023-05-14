using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class UnrecWeightingPhoto
{
    public long Id { get; set; }

    public int IdUnrecWeighing { get; set; }

    public string FileName { get; set; } = null!;

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int IdSettingsVideoSurveillance { get; set; }

    public virtual UnrecWeighing IdUnrecWeighingNavigation { get; set; } = null!;
}
