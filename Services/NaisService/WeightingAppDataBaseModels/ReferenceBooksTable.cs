using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ReferenceBooksTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NameTable { get; set; } = null!;

    public string? Description { get; set; }

    public bool HideInDictionariesList { get; set; }

    public string AccessTag { get; set; } = null!;
}
