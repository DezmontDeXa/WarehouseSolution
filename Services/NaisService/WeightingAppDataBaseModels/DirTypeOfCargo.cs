using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirTypeOfCargo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IdDirCargo { get; set; }

    public Guid Ref { get; set; }

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorages { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual DirCargo? IdDirCargoNavigation { get; set; }

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
