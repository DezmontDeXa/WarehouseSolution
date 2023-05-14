using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

/// <summary>
/// Таблица для настроек распознавания
/// </summary>
public partial class SettingsRecognitionMpixel
{
    public int Id { get; set; }

    public int? IdSettingsVideoSurveillance { get; set; }

    public bool Enable { get; set; }

    public int ZoneVisioLeft { get; set; }

    public int ZoneVisioTop { get; set; }

    public int ZoneVisioRight { get; set; }

    public int ZoneVisioBottom { get; set; }

    public bool ClearNumber { get; set; }

    public bool CorrectionSymbols { get; set; }

    public bool DeleteZones { get; set; }

    public bool CheckHeight { get; set; }

    public bool CheckProbability { get; set; }

    public bool CheckGipotize { get; set; }

    public bool AvoidReplay { get; set; }

    public int RecognitionProbabilityThreshold { get; set; }

    public int NumberFailedRecognitionCycles { get; set; }

    public int NumberSuccesedRecognitionCycles { get; set; }

    public int CarThreshold { get; set; }

    public int ZoneThreshold { get; set; }

    public int Thhory { get; set; }

    public int Thhorx { get; set; }

    public int Thbin { get; set; }

    public int MinPipeSize { get; set; }

    public int MaxPipeSize { get; set; }

    public bool ZoneMode96x24 { get; set; }

    public bool ZoneMode128x32 { get; set; }

    public bool ZoneMode160x40 { get; set; }

    public bool ZoneMode192x48 { get; set; }

    public bool ZoneMode256x64 { get; set; }

    public bool ZoneMode320x80 { get; set; }

    public bool ZoneMode384x96 { get; set; }

    public bool ZoneMode48x48 { get; set; }

    public bool ZoneMode64x64 { get; set; }

    public bool ZoneMode80x80 { get; set; }

    public bool ZoneMode96x96 { get; set; }

    public bool ZoneMode128x128 { get; set; }

    public bool ZoneMode160x160 { get; set; }

    public bool ZoneMode192x192 { get; set; }

    public DateTime UpdateDate { get; set; }

    public int UpdateByUserId { get; set; }

    public int IdDirWeightRoom { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
