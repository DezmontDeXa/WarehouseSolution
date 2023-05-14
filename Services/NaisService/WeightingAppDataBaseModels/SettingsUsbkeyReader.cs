using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsUsbkeyReader
{
    public int Id { get; set; }

    public bool Enable { get; set; }

    public string ComName { get; set; } = null!;

    public string Baudrate { get; set; } = null!;

    public string Parity { get; set; } = null!;

    public string Databits { get; set; } = null!;

    public string Stopbits { get; set; } = null!;

    public string UsbkeyReaderInterval { get; set; } = null!;

    public int IdDirWeightRoom { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedByUserId { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
