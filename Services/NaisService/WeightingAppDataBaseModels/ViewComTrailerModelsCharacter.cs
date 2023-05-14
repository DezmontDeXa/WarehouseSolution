using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ViewComTrailerModelsCharacter
{
    public int Id { get; set; }

    public string TrailerName { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }
}
