using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsSpeech
{
    public int Id { get; set; }

    public int SpeechType { get; set; }

    public bool Enable { get; set; }

    public int Volume { get; set; }

    public int Rate { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? UpdatedByUserId { get; set; }

    public string? MessageText { get; set; }

    public string? MessageTextTwo { get; set; }

    public string? MessageTextThree { get; set; }

    public string? MessageTextFour { get; set; }

    public string? MessageTextFive { get; set; }

    public string? MessageTextSix { get; set; }

    public string? MessageTextSeven { get; set; }

    public string? MessageTextEight { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
