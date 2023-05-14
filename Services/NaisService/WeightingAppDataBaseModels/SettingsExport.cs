using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsExport
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public bool ForDefinedPeriod { get; set; }

    public bool WithPeriodBeginning { get; set; }

    public int PeriodLength { get; set; }

    public DateTime PeriodBeginning { get; set; }

    public DateTime PeriodEnd { get; set; }

    public string FilePath { get; set; } = null!;

    public int TimePeriod { get; set; }

    public bool IsQeuryConfirmed { get; set; }

    public bool IsExecutedAfter { get; set; }

    public bool IsProtocolSwitched { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedByUserId { get; set; }
}
