using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.DataBaseModels;

public partial class BarrierInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Uri { get; set; } = null!;

    public int AreaId { get; set; }


    [ForeignKey(nameof(AreaId))]
    public virtual Area Area { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }
}
