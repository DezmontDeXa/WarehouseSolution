using System;
using System.Collections.Generic;

namespace Warehouse.Models;

public partial class Config
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string? Value { get; set; }
}
