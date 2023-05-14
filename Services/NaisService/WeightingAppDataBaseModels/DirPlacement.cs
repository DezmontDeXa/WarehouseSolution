using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirPlacement
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid Ref { get; set; }

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorageIdDirPlacementNavigations { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorageIdDirPlacementRecNavigations { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual ICollection<RegWeight> RegWeightIdDirPlacementNavigations { get; set; } = new List<RegWeight>();

    public virtual ICollection<RegWeight> RegWeightIdDirPlacementRecNavigations { get; set; } = new List<RegWeight>();
}
