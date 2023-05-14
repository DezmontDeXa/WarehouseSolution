using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class ComDirInvoiceDirStorage
{
    public int Id { get; set; }

    public int? IdDirCargo { get; set; }

    public int? IdDirInvoice { get; set; }

    public int? IdDirStrorage { get; set; }

    public int? IdDirPlacement { get; set; }

    public int? IdDirTypeOfPack { get; set; }

    public int? IdDirLoadPoint { get; set; }

    public int? IdDirUploadPoint { get; set; }

    public int? PackCount { get; set; }

    public float? PackPrice { get; set; }

    public bool Deleted { get; set; }

    public int? IdDirTypeOfCargo { get; set; }

    public float? DocTara { get; set; }

    public float? DocBrutto { get; set; }

    public float? DocNetto { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IdDirStrorageRec { get; set; }

    public int? IdDirPlacementRec { get; set; }

    public virtual DirCargo? IdDirCargoNavigation { get; set; }

    public virtual DirInvoice? IdDirInvoiceNavigation { get; set; }

    public virtual DirLoadPoint? IdDirLoadPointNavigation { get; set; }

    public virtual DirPlacement? IdDirPlacementNavigation { get; set; }

    public virtual DirPlacement? IdDirPlacementRecNavigation { get; set; }

    public virtual DirStorage? IdDirStrorageNavigation { get; set; }

    public virtual DirStorage? IdDirStrorageRecNavigation { get; set; }

    public virtual DirTypeOfCargo? IdDirTypeOfCargoNavigation { get; set; }

    public virtual DirTypeOfPack? IdDirTypeOfPackNavigation { get; set; }

    public virtual DirUploadPoint? IdDirUploadPointNavigation { get; set; }
}
