using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class TimeControledState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? CarStateId { get; set; }

    [ForeignKey(nameof(CarStateId))]
    public virtual CarState? CarState { get; set; }

    public int Timeout { get; set; }
}
