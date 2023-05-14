using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirFund
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();

    public virtual ICollection<WeighingInfo> WeighingInfos { get; set; } = new List<WeighingInfo>();
}
