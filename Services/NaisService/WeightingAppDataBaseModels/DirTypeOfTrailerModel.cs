using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirTypeOfTrailerModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdDirTypeOfTrailer { get; set; }

    public int IdDirTypeOfCarSuspension { get; set; }

    public float? Width { get; set; }

    public float? Height { get; set; }

    public float? Length { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public float? WeightAllowed { get; set; }

    public virtual ICollection<ComDirTransportDriverTrailer> ComDirTransportDriverTrailers { get; set; } = new List<ComDirTransportDriverTrailer>();

    public virtual ICollection<DirTrailerModelAxleCharacter> DirTrailerModelAxleCharacters { get; set; } = new List<DirTrailerModelAxleCharacter>();

    public virtual DirTypeOfCarSuspension IdDirTypeOfCarSuspensionNavigation { get; set; } = null!;

    public virtual DirTypeOfTrailer IdDirTypeOfTrailerNavigation { get; set; } = null!;

    public virtual ICollection<RegWeight> RegWeights { get; set; } = new List<RegWeight>();
}
