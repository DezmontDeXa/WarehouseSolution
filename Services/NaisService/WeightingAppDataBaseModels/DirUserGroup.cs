using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirUserGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool SystemRecord { get; set; }

    public virtual ICollection<ComDirUserGroupsAccessPermission> ComDirUserGroupsAccessPermissions { get; set; } = new List<ComDirUserGroupsAccessPermission>();

    public virtual ICollection<DirUser> DirUsers { get; set; } = new List<DirUser>();
}
