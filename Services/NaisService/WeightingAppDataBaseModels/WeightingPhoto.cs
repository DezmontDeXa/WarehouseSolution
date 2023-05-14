using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class WeightingPhoto
{
    public long Id { get; set; }

    public int IdWeighing { get; set; }

    public string FileName { get; set; } = null!;

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool FirstWeight { get; set; }

    public int IdSettingsVideoSurveillance { get; set; }

    public virtual Weighing IdWeighingNavigation { get; set; } = null!;
}
