using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirKfkhCombine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Make { get; set; }

    public float? Scope { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ComKfkh> ComKfkhs { get; set; } = new List<ComKfkh>();
}
