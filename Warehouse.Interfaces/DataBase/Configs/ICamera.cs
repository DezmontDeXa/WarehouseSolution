namespace Warehouse.Interfaces.DataBase.Configs
{
    public interface ICamera
    {
        int AreaId { get; set; }
        MoveDirection Direction { get; set; }
        int Id { get; set; }
        string Link { get; set; }
        string? Name { get; set; }
        int RoleId { get; set; }
    }
}