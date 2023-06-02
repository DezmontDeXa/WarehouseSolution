using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

[Index(nameof(PlateNumberForward), IsUnique = true)]
public partial class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string PlateNumberForward { get; set; } = null!;

    public string PlateNumberBackward { get; set; } = null!;

    public string? PlateNumberSimilars { get; set; }

    public string? Driver { get; set; }

    public int? TargetAreaId { get; set; }

    public virtual ICollection<WaitingList> WaitingLists { get; set; }

    public bool IsInspectionRequired { get; set; }

    public bool FirstWeighingCompleted { get; set; }

    public bool SecondWeighingCompleted { get; set; }

    public int? StorageId { get; set; }

    public int? CarStateId { get; set; }

    public int? AreaId { get; set; }

}
