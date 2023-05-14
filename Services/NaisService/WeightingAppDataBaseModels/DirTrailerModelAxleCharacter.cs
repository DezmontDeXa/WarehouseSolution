using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirTrailerModelAxleCharacter
{
    public int Id { get; set; }

    public int IdTypeOfTsmodels { get; set; }

    public int? WheelPitch { get; set; }

    public float? InterAxleDistance { get; set; }

    public int? AxleGroup { get; set; }

    public float? GroupInTones { get; set; }

    public float? LimitInTones { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual DirTypeOfTrailerModel IdTypeOfTsmodelsNavigation { get; set; } = null!;
}
