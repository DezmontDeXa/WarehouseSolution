namespace Warehouse.DBModels;

public partial class Camera
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Ip { get; set; }

    public bool UseSsl { get; set; }

    public string? Endpoint { get; set; }

    public string? Login { get; set; }

    public string ? Password { get; set; }

    public int? RoleId { get; set; }

    public int? AreaId { get; set; }
}
