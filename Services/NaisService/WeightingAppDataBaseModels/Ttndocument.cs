using System;
using System.Collections.Generic;

namespace NaisService.WeightingAppDataBaseModels;

public partial class Ttndocument
{
    public int Id { get; set; }

    public int IdWeighing { get; set; }

    public string? ProxyName { get; set; }

    public DateTime? ProxyDate { get; set; }

    public string? ProxyGivenBy { get; set; }

    public string? CargoReleaseName { get; set; }

    public string? CargoReleaseWorkAs { get; set; }

    public string? AccountantName { get; set; }

    public string? CargoPickUpName { get; set; }

    public string? CargoPickUpWorkAs { get; set; }

    public string? CargoPickUpIdentity { get; set; }

    public string? ConsigneeName { get; set; }

    public string? ConsigneeWorkAs { get; set; }

    public float? RouteDistance { get; set; }

    public float? CargoCapacity { get; set; }

    public string? TrailerNumber { get; set; }

    public string? TypeOfTrailer { get; set; }

    public string? CarriageDocumentNumber { get; set; }

    public string? PackSealNumber { get; set; }

    public string? LoadPoint { get; set; }

    public string? UploadPoint { get; set; }

    public string? CarrierOrganization { get; set; }

    public string? CustomerOrganization { get; set; }

    public string? ShipperOrganization { get; set; }

    public string? ConsigneeOrganization { get; set; }

    public string? PayerOrganization { get; set; }

    public byte? IdSystemDirTypeOfCarriage { get; set; }

    public byte? IdSystemDirTypeOfCarriageDocument { get; set; }

    public byte? IdSystemDirKindOfPacking { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? CargoPickUpAdditionalInfo { get; set; }

    public float? PriceCargo { get; set; }

    public float? PriceTransportation { get; set; }

    public bool? Forwarder { get; set; }

    public string? TypeOfOwn { get; set; }

    public string? OwnDir { get; set; }

    public string? OwnPositionDir { get; set; }

    public string? CarrierDir { get; set; }

    public string? CarrierPositionDir { get; set; }

    public string? CustomerDir { get; set; }

    public string? CustomerPositionDir { get; set; }

    public string? ShipperDir { get; set; }

    public string? ShipperPositionDir { get; set; }

    public string? ConsigneeDir { get; set; }

    public string? ConsigneePositionDir { get; set; }

    public string? PayerDir { get; set; }

    public string? PayerPositionDir { get; set; }

    public virtual SystemDirKindOfPacking? IdSystemDirKindOfPackingNavigation { get; set; }

    public virtual SystemDirTypeOfCarriageDocument? IdSystemDirTypeOfCarriageDocumentNavigation { get; set; }

    public virtual SystemDirTypeOfCarriage? IdSystemDirTypeOfCarriageNavigation { get; set; }

    public virtual Weighing IdWeighingNavigation { get; set; } = null!;
}
