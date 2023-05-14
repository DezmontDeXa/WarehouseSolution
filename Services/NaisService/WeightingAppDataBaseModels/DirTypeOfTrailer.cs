using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirTypeOfTrailer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ComDirTransportDriverTrailer> ComDirTransportDriverTrailers { get; set; } = new List<ComDirTransportDriverTrailer>();

    public virtual ICollection<DirTypeOfTrailerModel> DirTypeOfTrailerModels { get; set; } = new List<DirTypeOfTrailerModel>();

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
