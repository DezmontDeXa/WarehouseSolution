using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class Weighing
{
    public int Id { get; set; }

    public int IdWeightingInfo { get; set; }

    public float FirstWeight { get; set; }

    public float? HandleFirstWeight { get; set; }

    public int? NumberOfWeight { get; set; }

    public bool FlagWeight { get; set; }

    public bool Deleted { get; set; }

    public float? SecondWeight { get; set; }

    public float? HandleSecondWeight { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int IdFirstWeightUser { get; set; }

    public int? IdSecondWeightUser { get; set; }

    public int? IdRegWeight { get; set; }

    public DateTime DateTimeFirstWeight { get; set; }

    public DateTime? DateTimeSecondWeight { get; set; }

    public Guid Ref { get; set; }

    public bool? FirstWeight1C { get; set; }

    public bool? SecondWeight1C { get; set; }

    public virtual DirUser IdFirstWeightUserNavigation { get; set; } = null!;

    public virtual RegWeight? IdRegWeightNavigation { get; set; }

    public virtual DirUser? IdSecondWeightUserNavigation { get; set; }

    public virtual WeighingInfo IdWeightingInfoNavigation { get; set; } = null!;

    public virtual ICollection<InvoiceDocument> InvoiceDocuments { get; set; } = new List<InvoiceDocument>();

    public virtual ICollection<InvoiceTorg12Document> InvoiceTorg12Documents { get; set; } = new List<InvoiceTorg12Document>();

    public virtual ICollection<Ttndocument> Ttndocuments { get; set; } = new List<Ttndocument>();

    public virtual ICollection<WeightingPhoto> WeightingPhotos { get; set; } = new List<WeightingPhoto>();
}
