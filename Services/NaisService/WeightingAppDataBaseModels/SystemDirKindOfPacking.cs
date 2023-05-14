using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SystemDirKindOfPacking
{
    public byte Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Ttndocument> Ttndocuments { get; set; } = new List<Ttndocument>();
}
