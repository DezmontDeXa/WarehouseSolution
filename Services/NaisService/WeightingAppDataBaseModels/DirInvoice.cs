using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirInvoice
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdDirCarrier { get; set; }

    public int? IdDirCounterparty { get; set; }

    public int? IdDirTypeOfOperation { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public float? Price { get; set; }

    public int? Discount { get; set; }

    public float? TotalCost { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ComDirInvoiceDirStorage> ComDirInvoiceDirStorages { get; set; } = new List<ComDirInvoiceDirStorage>();

    public virtual DirCarrier? IdDirCarrierNavigation { get; set; }

    public virtual DirCounterparty? IdDirCounterpartyNavigation { get; set; }

    public virtual DirTypeOfOperation? IdDirTypeOfOperationNavigation { get; set; }

    public virtual ICollection<WeighingInfo> WeighingInfos { get; set; } = new List<WeighingInfo>();
}
