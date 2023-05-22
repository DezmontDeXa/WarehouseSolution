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

    [ForeignKey(nameof(CarId))]
    public virtual Car? Car { get; set; }

    public int? CarStateId { get; set; }

    [ForeignKey(nameof(CarStateId))]
    public virtual CarState? CarState { get; set; }

    public int? TimeControledStateId { get; set; }

    [ForeignKey(nameof(TimeControledStateId))]
    public virtual TimeControledState? TimeControledState { get; set; }

    public DateTime StartTime { get; set; }
}
