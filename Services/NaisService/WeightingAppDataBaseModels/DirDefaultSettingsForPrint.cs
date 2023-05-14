using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirDefaultSettingsForPrint
{
    public int Id { get; set; }

    public string TypeOfField { get; set; } = null!;

    public string FieldValue { get; set; } = null!;
}
