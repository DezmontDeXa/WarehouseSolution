using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ComDirUserGroupsAccessPermission
{
    public int Id { get; set; }

    public int IdDirUserGroup { get; set; }

    public int IdSystemDirAccessPermission { get; set; }

    public virtual DirUserGroup IdDirUserGroupNavigation { get; set; } = null!;

    public virtual SystemDirAccessPermission IdSystemDirAccessPermissionNavigation { get; set; } = null!;
}
