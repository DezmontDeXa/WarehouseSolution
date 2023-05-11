﻿using System;
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

    public string? Driver { get; set; }

    public string CarStateContext { get; set; }

    public virtual ICollection<WaitingList> WaitingLists { get; set; }

    public bool IsInspectionRequired { get; set; }

    public bool FirstWeighingCompleted { get; set; }

    public bool SecondWeighingCompleted { get; set; }


    public int? CarStateId { get; set; }

    [ForeignKey(nameof(CarStateId))]
    public virtual CarState? CarState { get; set; }


    public int? AreaId { get; set; }

    [ForeignKey(nameof(AreaId))]
    public virtual Area? Area { get; set; }

    public Car()
    {
        CarStateContext = string.Empty;
    }
}
