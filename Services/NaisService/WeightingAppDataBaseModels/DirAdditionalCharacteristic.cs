using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirAdditionalCharacteristic
{
    public int Id { get; set; }

    public float? Humidity { get; set; }

    public float? Sor { get; set; }

    public float? Marriage { get; set; }

    public string? ReasonOfMarriage { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();

    public virtual ICollection<WeighingInfo> WeighingInfos { get; set; } = new List<WeighingInfo>();
}
