using System;
using System.Collections.Generic;

namespace Warehouse.SharedLibrary;

public partial class Car
{
    public int Id { get; set; }

    public string PlateNumberForward { get; set; } = null!;

    public string PlateNumberBackward { get; set; } = null!;

    public string? PlateNumberSimilars { get; set; }

    public int CarStateId { get; set; }
}
