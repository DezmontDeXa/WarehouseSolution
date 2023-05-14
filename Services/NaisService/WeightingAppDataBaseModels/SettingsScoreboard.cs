using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsScoreboard
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public string ScoreboardModelValue { get; set; } = null!;

    public string ComName { get; set; } = null!;

    public string Baudrate { get; set; } = null!;

    public string Parity { get; set; } = null!;

    public string Databits { get; set; } = null!;

    public string Stopbits { get; set; } = null!;

    public string ScoreboardInterval { get; set; } = null!;

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedByUserId { get; set; }

    public string InterfaceType { get; set; } = null!;

    public bool Rts { get; set; }

    public bool Dtr { get; set; }

    public string Handshake { get; set; } = null!;

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
