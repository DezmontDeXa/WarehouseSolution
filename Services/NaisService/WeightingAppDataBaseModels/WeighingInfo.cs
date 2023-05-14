using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class WeighingInfo
{
    public int Id { get; set; }

    public int? IdDirInvoice { get; set; }

    public int? IdDirKfkh { get; set; }

    public int? IdDirLaboratory { get; set; }

    public int? IdDirAdditionalCharacteristics { get; set; }

    public bool Deleted { get; set; }

    public int IdComTransportDriverTrailer { get; set; }

    public int? IdDirFund { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<ComKfkh> ComKfkhs { get; set; } = new List<ComKfkh>();

    public virtual ComDirTransportDriverTrailer IdComTransportDriverTrailerNavigation { get; set; } = null!;

    public virtual DirAdditionalCharacteristic? IdDirAdditionalCharacteristicsNavigation { get; set; }

    public virtual DirFund? IdDirFundNavigation { get; set; }

    public virtual DirInvoice? IdDirInvoiceNavigation { get; set; }

    public virtual ICollection<Weighing> Weighings { get; set; } = new List<Weighing>();
}
