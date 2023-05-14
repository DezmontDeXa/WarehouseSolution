using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirStorage
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Company { get; set; }

    public string? SeniorStorekeeper { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid Ref { get; set; }

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorageIdDirStrorageNavigations { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorageIdDirStrorageRecNavigations { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual ICollection<RegWeight> RegWeightIdDirStorageNavigations { get; set; } = new List<RegWeight>();

    public virtual ICollection<RegWeight> RegWeightIdDirStorageRecNavigations { get; set; } = new List<RegWeight>();
}
