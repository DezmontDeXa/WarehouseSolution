using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.ConfigDataBase.Models;

public partial class Area : IArea
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }
}
