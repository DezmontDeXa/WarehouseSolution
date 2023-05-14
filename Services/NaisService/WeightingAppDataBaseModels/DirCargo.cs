using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirCargo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? PriceTonne { get; set; }

    public int? SellingPrice { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public Guid Ref { get; set; }

    public int? GabaritLength { get; set; }

    public int? GabaritWidth { get; set; }

    public int? GabaritHeight { get; set; }

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorages { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual ICollection<DirTypeOfCargo> DirTypeOfCargos { get; set; } = new List<DirTypeOfCargo>();

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
