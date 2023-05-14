using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsTerminal
{
    public int Id { get; set; }

    public string? TerminalModelValue { get; set; }

    public string InterfaceType { get; set; } = null!;

    public string NetworkAddress { get; set; } = null!;

    public float MultiplierWeight { get; set; }

    public string ComName { get; set; } = null!;

    public string Baudrate { get; set; } = null!;

    public string Parity { get; set; } = null!;

    public string Databits { get; set; } = null!;

    public string Stopbits { get; set; } = null!;

    public string Handshake { get; set; } = null!;

    public bool Rts { get; set; }

    public bool Dtr { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime UpdatedDate { get; set; }

    public bool Enable { get; set; }

    public string TerminalInterval { get; set; } = null!;

    public int UpdatedByUserId { get; set; }

    public float WeightConditionalZero { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
