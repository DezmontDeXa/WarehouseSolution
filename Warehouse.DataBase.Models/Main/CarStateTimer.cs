using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.DataBase.Models.Main;

public partial class CarStateTimer : ICarStateTimer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool IsAlive { get; set; }

    public int? CarId { get; set; }

    public int? CarStateId { get; set; }

    public int? TimeControledStateId { get; set; }

    public long StartTimeTicks { get; set; }
}
