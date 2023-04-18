using System;
using System.Collections.Generic;

namespace Warehouse.DataBaseModels;

public partial class WaitingList
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AccessGrantType { get; set; }
}
