using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string PlateNumberForward { get; set; } = null!;

    public string PlateNumberBackward { get; set; } = null!;

    public string? PlateNumberSimilars { get; set; }

    public int? CarStateId { get; set; }

    [ForeignKey(nameof(CarStateId))]
    public virtual CarState? State { get; set; }


    public int? AreaId { get; set; }

    [ForeignKey(nameof(AreaId))]
    public virtual Area? Area { get; set; }

    public virtual ICollection<WaitingList> WaitingLists { get; set; }
}
