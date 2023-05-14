using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class RegWeight
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? DocumentId { get; set; }

    public DateTime? DatePassExpiry { get; set; }

    public TimeSpan? TimePassExpiry { get; set; }

    public bool? PassEnabled { get; set; }

    public bool AutoSaveTara { get; set; }

    public float? Tara { get; set; }

    public float? DocTara { get; set; }

    public float? DocBrutto { get; set; }

    public float? DocNetto { get; set; }

    public string? InvoiceName { get; set; }

    public byte IdSystemDirTypeOfRegweight { get; set; }

    public byte IdSystemDirIdentifierState { get; set; }

    public byte IdSystemDirTypeOfPass { get; set; }

    public int? IdDirKfkhPlantation { get; set; }

    public int? IdDirAdditionalCharacteristics { get; set; }

    public int? IdDirFund { get; set; }

    public int? IdDirCargo { get; set; }

    public int? IdDirTypeOfCargo { get; set; }

    public int? IdDirStorage { get; set; }

    public int? IdDirStorageRec { get; set; }

    public int? IdDirPlacement { get; set; }

    public int? IdDirPlacementRec { get; set; }

    public int? IdDirDriver { get; set; }

    public int IdDirTransport { get; set; }

    public int? IdDirTrailer { get; set; }

    public int? IdDirTypeOfTrailer { get; set; }

    public int? IdDirTypeOfTransport { get; set; }

    public int? IdDirCarrier { get; set; }

    public int? IdDirCounterparty { get; set; }

    public int? IdDirTypeOfOperation { get; set; }

    public bool Deleted { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? IdDirTypeOfTransportModels { get; set; }

    public int? IdDirTypeOfTrailerModel { get; set; }

    public virtual DirAdditionalCharacteristic? IdDirAdditionalCharacteristicsNavigation { get; set; }

    public virtual DirCargo? IdDirCargoNavigation { get; set; }

    public virtual DirCarrier? IdDirCarrierNavigation { get; set; }

    public virtual DirCounterparty? IdDirCounterpartyNavigation { get; set; }

    public virtual DirDriver? IdDirDriverNavigation { get; set; }

    public virtual DirFund? IdDirFundNavigation { get; set; }

    public virtual DirKfkhPlantation? IdDirKfkhPlantationNavigation { get; set; }

    public virtual DirPlacement? IdDirPlacementNavigation { get; set; }

    public virtual DirPlacement? IdDirPlacementRecNavigation { get; set; }

    public virtual DirStorage? IdDirStorageNavigation { get; set; }

    public virtual DirStorage? IdDirStorageRecNavigation { get; set; }

    public virtual DirTrailer? IdDirTrailerNavigation { get; set; }

    public virtual DirTransport IdDirTransportNavigation { get; set; } = null!;

    public virtual DirTypeOfCargo? IdDirTypeOfCargoNavigation { get; set; }

    public virtual DirTypeOfOperation? IdDirTypeOfOperationNavigation { get; set; }

    public virtual DirTypeOfTrailerModel? IdDirTypeOfTrailerModelNavigation { get; set; }

    public virtual DirTypeOfTrailer? IdDirTypeOfTrailerNavigation { get; set; }

    public virtual DirTypeOfTransportModel? IdDirTypeOfTransportModelsNavigation { get; set; }

    public virtual DirTypeOfTransport? IdDirTypeOfTransportNavigation { get; set; }

    public virtual SystemDirIdentifierState IdSystemDirIdentifierStateNavigation { get; set; } = null!;

    public virtual SystemDirTypeOfPass IdSystemDirTypeOfPassNavigation { get; set; } = null!;

    public virtual SystemDirTypeOfRegweight IdSystemDirTypeOfRegweightNavigation { get; set; } = null!;

    public virtual ICollection<Weighing> Weighings { get; set; } = new List<Weighing>();
}
