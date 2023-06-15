using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.DataBase.Models.Main;

public partial class CameraRole : ICameraRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? TypeName { get; set; }
}
