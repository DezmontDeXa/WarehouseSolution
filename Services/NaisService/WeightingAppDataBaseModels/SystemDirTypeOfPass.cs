using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SystemDirTypeOfPass
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
