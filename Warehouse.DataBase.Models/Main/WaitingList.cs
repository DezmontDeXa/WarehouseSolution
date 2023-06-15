using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.DataBase.Models.Main;

public partial class WaitingList : IWaitingList
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public AccessGrantType AccessGrantType { get; set; }

    public virtual ICollection<Car> Cars { get; set; }
    ICollection<ICar> IWaitingList.Cars => Cars.Cast<ICar>().ToList();

    public int Number { get; set; }

    public string? Customer { get; set; }

    public string? PurposeOfArrival { get; set; }

    public string? Ship { get; set; }

    public string? Route { get; set; }

    public string? Camera { get; set; }

    public DateTime? Date { get; set; }
}
