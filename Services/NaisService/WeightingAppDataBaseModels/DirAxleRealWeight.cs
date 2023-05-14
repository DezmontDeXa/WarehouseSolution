using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirAxleRealWeight
{
    public int Id { get; set; }

    public int? IdWeighings { get; set; }

    public string Weight { get; set; } = null!;

    public int? IdDirTypeOfTransportModels { get; set; }

    public int? IdDirTypeOfTrailerModels { get; set; }

    public DateTime CreatedDate { get; set; }
}
