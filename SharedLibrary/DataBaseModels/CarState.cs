using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class CarState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? AreaId { get; set; }

    [ForeignKey(nameof(AreaId))]
    public virtual Area? Area { get; set; }
}
