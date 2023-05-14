using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirUser
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Deleted { get; set; }

    public int IdDirWeightRoom { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IdDirUserGroups { get; set; }

    public Guid Ref { get; set; }

    public virtual ICollection<AuditWeighing> AuditWeighings { get; set; } = new List<AuditWeighing>();

    public virtual DirUserGroup? IdDirUserGroupsNavigation { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;

    public virtual ICollection<UnrecWeighing> UnrecWeighings { get; set; } = new List<UnrecWeighing>();

    public virtual ICollection<Weighing> WeighingIdFirstWeightUserNavigations { get; set; } = new List<Weighing>();

    public virtual ICollection<Weighing> WeighingIdSecondWeightUserNavigations { get; set; } = new List<Weighing>();
}
