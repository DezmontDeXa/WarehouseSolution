namespace Warehouse.Interfaces.DataBase.Configs
{
    public interface IStorage
    {
        int? AreaId { get; set; }
        int Id { get; set; }
        string NaisCode { get; set; }
        string Name { get; set; }
    }
}