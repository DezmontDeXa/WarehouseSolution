using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirTypeOfOperation
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public byte IdSystemDirDirectionTypeOperation { get; set; }

    public bool SystemRecord { get; set; }

    public bool Main { get; set; }

    public virtual ICollection<DirInvoice> DirInvoices { get; set; } = new List<DirInvoice>();

    public virtual SystemDirDirectionTypeOperation IdSystemDirDirectionTypeOperationNavigation { get; set; } = null!;

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
