using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class UnrecWeighing
{
    public int Id { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? PlatformNumber { get; set; }

    public int? IdDirWeightRoom { get; set; }

    public float? StableWeight { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual DirUser CreatedByUser { get; set; } = null!;

    public virtual DirWeightRoom? IdDirWeightRoomNavigation { get; set; }

    public virtual ICollection<UnrecWeightingPhoto> UnrecWeightingPhotos { get; set; } = new List<UnrecWeightingPhoto>();
}
