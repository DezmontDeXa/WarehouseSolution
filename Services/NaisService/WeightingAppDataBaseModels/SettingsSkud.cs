using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsSkud
{
    public int Id { get; set; }

    public bool EnableSkud { get; set; }

    public string InterfaceType { get; set; } = null!;

    public string NetworkAddress { get; set; } = null!;

    public string ComName { get; set; } = null!;

    public string Baudrate { get; set; } = null!;

    public string Parity { get; set; } = null!;

    public string Databits { get; set; } = null!;

    public string Stopbits { get; set; } = null!;

    public string Handshake { get; set; } = null!;

    public bool EnableBarrier { get; set; }

    public string BarrierInOpen { get; set; } = null!;

    public string BarrierInClose { get; set; } = null!;

    public string BarrierOutOpen { get; set; } = null!;

    public string BarrierOutClose { get; set; } = null!;

    public string BarrierWorkMode { get; set; } = null!;

    public bool EnableTrafficLight { get; set; }

    public string TrafficLightInNear { get; set; } = null!;

    public string TrafficLightInFar { get; set; } = null!;

    public string TrafficLightOutNear { get; set; } = null!;

    public string TrafficLightOutFar { get; set; } = null!;

    public string TrafficLightWorkMode { get; set; } = null!;

    public bool EnableReader { get; set; }

    public string ReaderIn { get; set; } = null!;

    public string ReaderCenter { get; set; } = null!;

    public string ReaderOut { get; set; } = null!;

    public string ReaderViewMode { get; set; } = null!;

    public string ReaderWorkMode { get; set; } = null!;

    public bool EnableIrSensors { get; set; }

    public bool ControlS1 { get; set; }

    public bool ControlS2 { get; set; }

    public bool ControlS3 { get; set; }

    public bool ControlS4 { get; set; }

    public bool ControlS5 { get; set; }

    public bool ControlS6 { get; set; }

    public bool InvertS1 { get; set; }

    public bool InvertS2 { get; set; }

    public bool InvertS3 { get; set; }

    public bool InvertS4 { get; set; }

    public bool InvertS5 { get; set; }

    public bool InvertS6 { get; set; }

    public int PlatformNumber { get; set; }

    public int IdDirWeightRoom { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int UpdatedByUserId { get; set; }

    public bool BarrierState { get; set; }

    public bool TrafficState { get; set; }

    public virtual DirWeightRoom IdDirWeightRoomNavigation { get; set; } = null!;
}
