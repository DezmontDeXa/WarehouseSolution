using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.DataBase.Models.Main.Notifies;

public abstract class CarNotifyBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool IsProcessed { get; set; }

}
