using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Warehouse.Interfaces.Barriers;

namespace Warehouse.DataBase.Models.Config;

public partial class BarrierInfo : IBarrierInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Uri { get; set; } = null!;

    public int AreaId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }
}
