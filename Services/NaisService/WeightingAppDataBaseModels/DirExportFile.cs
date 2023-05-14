using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirExportFile
{
    public int Id { get; set; }

    public string PatternName { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public int FileExtension { get; set; }

    public bool Export { get; set; }

    public string CommandText { get; set; } = null!;

    public DateTime? BeginTime { get; set; }

    public DateTime? EndTime { get; set; }
}
