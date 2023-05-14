using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SystemDirDirectionTypeOperation
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DirTypeOfOperation> DirTypeOfOperations { get; set; } = new List<DirTypeOfOperation>();
}
