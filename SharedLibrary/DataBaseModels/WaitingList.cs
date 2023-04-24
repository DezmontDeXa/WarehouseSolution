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

    public virtual List<Car> Cars { get; set; }
}
