using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirCarrier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Inn { get; set; }

    public string? Kpp { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? CheckingAccount { get; set; }

    public string? Bik { get; set; }

    public string? CorrespondentAccount { get; set; }

    public string? Okpo { get; set; }

    public string? FullName { get; set; }

    public string? Fiodirector { get; set; }

    public string? FiochiefAccountant { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Country { get; set; }

    public string? DirPosition { get; set; }

    public virtual ICollection<DirInvoice> DirInvoices { get; set; } = new List<DirInvoice>();

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
