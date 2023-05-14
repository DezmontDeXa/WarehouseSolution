using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ComKfkh
{
    public int Id { get; set; }

    public int? IdDirCombine { get; set; }

    public int? IdDirCombiner { get; set; }

    public int? IdDirPlantation { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int IdWeighigInfo { get; set; }

    public float? UseScope { get; set; }

    public virtual DirKfkhCombine? IdDirCombineNavigation { get; set; }

    public virtual DirKfkhCombiner? IdDirCombinerNavigation { get; set; }

    public virtual DirKfkhPlantation? IdDirPlantationNavigation { get; set; }

    public virtual WeighingInfo IdWeighigInfoNavigation { get; set; } = null!;
}
