using System;
using System.Collections.Generic;

namespace Warehouse.DataBaseModels;

public partial class CameraRole
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? TypeName { get; set; }
}
