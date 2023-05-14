using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class DirWeightRoom
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<AuditWeighing> AuditWeighings { get; set; } = new List<AuditWeighing>();

    public virtual ICollection<DirUser> DirUsers { get; set; } = new List<DirUser>();

    public virtual ICollection<SettingsAuditLoad> SettingsAuditLoads { get; set; } = new List<SettingsAuditLoad>();

    public virtual ICollection<SettingsAxleLoad> SettingsAxleLoads { get; set; } = new List<SettingsAxleLoad>();

    public virtual ICollection<SettingsRecognitionModule> SettingsRecognitionModules { get; set; } = new List<SettingsRecognitionModule>();

    public virtual ICollection<SettingsRecognitionMpixel> SettingsRecognitionMpixels { get; set; } = new List<SettingsRecognitionMpixel>();

    public virtual ICollection<SettingsScoreboard> SettingsScoreboards { get; set; } = new List<SettingsScoreboard>();

    public virtual ICollection<SettingsSkud> SettingsSkuds { get; set; } = new List<SettingsSkud>();

    public virtual ICollection<SettingsSpeech> SettingsSpeeches { get; set; } = new List<SettingsSpeech>();

    public virtual ICollection<SettingsTerminal> SettingsTerminals { get; set; } = new List<SettingsTerminal>();

    public virtual ICollection<SettingsUsbkeyReader> SettingsUsbkeyReaders { get; set; } = new List<SettingsUsbkeyReader>();

    public virtual ICollection<SettingsVideoSurveillance> SettingsVideoSurveillances { get; set; } = new List<SettingsVideoSurveillance>();

    public virtual ICollection<UnrecWeighing> UnrecWeighings { get; set; } = new List<UnrecWeighing>();
}
