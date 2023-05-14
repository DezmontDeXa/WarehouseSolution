using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsApp
{
    public int Id { get; set; }

    public bool TsnumberMaskUse { get; set; }

    public string TsnumberMask { get; set; } = null!;

    public bool NeedOperationType { get; set; }

    public bool NeedFund { get; set; }

    public bool NeedCargoType { get; set; }

    public bool NeedPlacement { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IdCompany { get; set; }

    public string? PhotoDirectory { get; set; }

    public float ConditionalZero { get; set; }

    public bool UseComObject { get; set; }

    public bool UseRegion { get; set; }

    public bool UnionRecognizeSkud { get; set; }

    public virtual DirCounterparty? IdCompanyNavigation { get; set; }
}
