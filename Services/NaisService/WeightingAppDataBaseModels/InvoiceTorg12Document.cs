using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class InvoiceTorg12Document
{
    public int Id { get; set; }

    public int IdWeighing { get; set; }

    public string? Name { get; set; }

    public DateTime Date { get; set; }

    public float? Nds { get; set; }

    public string? StructName { get; set; }

    public string? Foundation { get; set; }

    public string? CargoReleaseName { get; set; }

    public string? CargoReleaseWorkAs { get; set; }

    public string? AccountantName { get; set; }

    public string? ShipperName { get; set; }

    public string? ShipperWorkAs { get; set; }

    public string? CargoPickUpName { get; set; }

    public string? CargoPickUpWorkAs { get; set; }

    public string? ConsigneeName { get; set; }

    public string? ConsigneeWorkAs { get; set; }

    public string? ProxyName { get; set; }

    public DateTime? ProxyDate { get; set; }

    public string? ProxyGivenBy { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Weighing IdWeighingNavigation { get; set; } = null!;
}
