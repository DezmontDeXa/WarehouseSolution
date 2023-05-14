using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class AuditWeighing
{
    public int Id { get; set; }

    public float AuditWeight { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? PlatformNumber { get; set; }

    public int? IdDirWeightRoom { get; set; }

    public float? AuditStableWeight { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? TansportNum { get; set; }

    public bool IsRegistered { get; set; }

    public virtual ICollection<AuditWeightingPhoto> AuditWeightingPhotos { get; set; } = new List<AuditWeightingPhoto>();

    public virtual DirUser CreatedByUser { get; set; } = null!;

    public virtual DirWeightRoom? IdDirWeightRoomNavigation { get; set; }
}
