using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SystemDirAccessPermission
{
    public int Id { get; set; }

    /// <summary>
    /// Название разрешения
    /// </summary>
    public string Name { get; set; } = null!;

    public string ControlTag { get; set; } = null!;

    public virtual ICollection<ComDirUserGroupsAccessPermission> ComDirUserGroupsAccessPermissions { get; set; } = new List<ComDirUserGroupsAccessPermission>();
}
