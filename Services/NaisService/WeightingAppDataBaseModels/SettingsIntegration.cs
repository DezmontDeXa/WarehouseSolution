using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsIntegration
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public bool ForDefinedPeriod { get; set; }

    public int? PeriodLength { get; set; }

    public DateTime? PeriodBeginning { get; set; }

    public DateTime? PeriodEnd { get; set; }

    public string? DownloadFilePath { get; set; }

    public string? UploadFilePath { get; set; }

    public int TimePeriod { get; set; }

    public int DownloadPeriodTime { get; set; }

    public bool IsUploadExecuted { get; set; }

    public bool IsDownloadExecuted { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedByUserId { get; set; }

    public DateTime? BeginTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? WithCounterparty { get; set; }

    public bool? WithAdditionalCharachteristics { get; set; }

    public bool? WithCargo { get; set; }

    public bool? WithStorage { get; set; }

    public bool? WithUsers { get; set; }

    public bool? WithWeighing { get; set; }
}
