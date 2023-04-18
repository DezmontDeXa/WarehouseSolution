using System;
using System.Collections.Generic;

namespace Warehouse.DataBaseModels;

public partial class WaitingListToCar
{
    public int WaitingListId { get; set; }

    public int CarId { get; set; }
}
