using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.ConfigDataBase.Models;

public partial class TimeControledState : ITimeControledState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? CarStateId { get; set; }

    public int Timeout { get; set; }
}
