using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.DataBase.Models.Main;

[Index(nameof(PlateNumberForward), IsUnique = true)]
public partial class Car : ICar
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

    ICollection<IWaitingList> ICar.WaitingLists => WaitingLists.Cast<IWaitingList>().ToList();

    public bool IsInspectionRequired { get; set; }

    public bool FirstWeighingCompleted { get; set; }

    public bool SecondWeighingCompleted { get; set; }

    public int? StorageId { get; set; }

    public int? CarStateId { get; set; }

    public int? AreaId { get; set; }
}
