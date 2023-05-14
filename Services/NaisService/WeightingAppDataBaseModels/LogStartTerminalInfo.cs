using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class LogStartTerminalInfo
{
    public int Id { get; set; }

    public string? Date { get; set; }

    public string? Time { get; set; }

    public int? Temperature { get; set; }

    public float? SupplyVoltage { get; set; }

    public int? NumberOfSensors { get; set; }

    public int? SerialNumberTerminal { get; set; }

    public int? SerialNumberLibra { get; set; }

    public string? DateVerificationProtocolFormation { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
