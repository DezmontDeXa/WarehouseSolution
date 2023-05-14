using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsEmail
{
    public int Id { get; set; }

    public string UserMail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? AdresantName { get; set; }

    public string AdresantMail { get; set; } = null!;

    public string? MessageBody { get; set; }

    public string? Message { get; set; }

    public int PortYandex { get; set; }

    public int PortMail { get; set; }

    public int PortGmail { get; set; }

    public bool Enable { get; set; }

    public int TimePeriod { get; set; }

    public bool PeriodFlag { get; set; }

    public bool SendFlag { get; set; }

    public bool ExcelFlag { get; set; }

    public int EventTypeId { get; set; }

    public string? UserSmtp { get; set; }

    public bool UserUseSsl { get; set; }

    public int UserPort { get; set; }

    public bool CustomMail { get; set; }
}
