using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class InvoiceDocument
{
    public int Id { get; set; }

    public int IdWeighing { get; set; }

    public string? Name { get; set; }

    public DateTime Date { get; set; }

    public float? Nds { get; set; }

    public string? DocumentName { get; set; }

    public string? ChiefName { get; set; }

    public string? AccountantName { get; set; }

    public string? BusinessmanName { get; set; }

    public string? RegIpcertificate { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Weighing IdWeighingNavigation { get; set; } = null!;
}
