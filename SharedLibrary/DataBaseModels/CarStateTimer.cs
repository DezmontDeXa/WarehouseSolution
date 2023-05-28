using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class CarStateTimer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool IsAlive { get; set; }

    public int? CarId { get; set; }

    public int? CarStateId { get; set; }

    public int? TimeControledStateId { get; set; }

    public DateTime StartTime { get; set; }
}
