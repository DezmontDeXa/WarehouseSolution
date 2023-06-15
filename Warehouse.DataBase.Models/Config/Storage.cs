using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.DataBase.Models.Config;

public class Storage : IStorage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public string NaisCode { get; set; }

    public int? AreaId { get; set; }

}
