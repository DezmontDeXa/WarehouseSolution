using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirKfkhCombiner
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? SeriesOfPassport { get; set; }

    public string? IdOfPassport { get; set; }

    public string? IssuedBy { get; set; }

    public string? ResidenceAddress { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? IssuedWhen { get; set; }

    public string? DriverLicenseSeries { get; set; }

    public string? DriverLicenseNumber { get; set; }

    public virtual ICollection<ComKfkh> ComKfkhs { get; set; } = new List<ComKfkh>();
}
