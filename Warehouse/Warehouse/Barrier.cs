using System;
using System.Collections.Generic;

namespace Warehouse;

public partial class Barrier
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string OpenLink { get; set; } = null!;

    public int AreaId { get; set; }
}
