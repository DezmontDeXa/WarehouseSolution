using System;
using System.Collections.Generic;

namespace Warehouse.SharedLibrary;

public partial class BarrierInfo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Uri { get; set; } = null!;

    public int AreaId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }
}
