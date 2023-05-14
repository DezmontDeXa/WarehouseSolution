using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ComDirTransportDriverTrailer
{
    public int Id { get; set; }

    public int? IdDirDriver { get; set; }

    public int IdDirTransport { get; set; }

    public int? IdDirTrailer { get; set; }

    public bool Deleted { get; set; }

    public int? IdDirTypeOfTrailer { get; set; }

    public int? IdDirTypeOfTransport { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? RecognTransportNumFirstWeight { get; set; }

    public string? RecognTrailerNumFirstWeight { get; set; }

    public string? RecognTransportNumSecondWeight { get; set; }

    public string? RecognTrailerNumSecondWeight { get; set; }

    public int? IdTransportModel { get; set; }

    public int? IdTrailerModel { get; set; }

    public virtual DirDriver? IdDirDriverNavigation { get; set; }

    public virtual DirTrailer? IdDirTrailerNavigation { get; set; }

    public virtual DirTransport IdDirTransportNavigation { get; set; } = null!;

    public virtual DirTypeOfTrailer? IdDirTypeOfTrailerNavigation { get; set; }

    public virtual DirTypeOfTransport? IdDirTypeOfTransportNavigation { get; set; }

    public virtual DirTypeOfTrailerModel? IdTrailerModelNavigation { get; set; }

    public virtual DirTypeOfTransportModel? IdTransportModelNavigation { get; set; }

    public virtual ICollection<WeighingInfo> WeighingInfos { get; set; } = new List<WeighingInfo>();
}
