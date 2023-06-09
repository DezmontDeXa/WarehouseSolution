using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DataBaseModels;

public partial class WaitingList
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public AccessGrantType AccessGrantType { get; set; }

    public virtual ICollection<Car> Cars { get; set; }

    public int Number { get; set; }

    public string? Customer { get; set; }

    public string? PurposeOfArrival { get; set; }

    public string? Ship { get; set; }

    public string? Route { get; set; }

    public string? Camera { get; set; }

    public DateTime? Date { get; set; }

}
