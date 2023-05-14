using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class SettingsPreAxleReport
{
    public int Id { get; set; }

    public string? PpvkNumber { get; set; }

    public string? ScaleNamber { get; set; }

    public string? PpvkOwner { get; set; }

    public string? CertificateNumber { get; set; }

    public string? CertificateName { get; set; }

    public string? CertificateOrg { get; set; }

    public DateTime CertificateFrom { get; set; }

    public DateTime CertificateTo { get; set; }

    public string? ControlPlace { get; set; }

    public string? WeghingMode { get; set; }
}
