using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class Camera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Ip { get; set; } = null!;

    public int RoleId { get; set; }


    [ForeignKey(nameof(RoleId))]
    public virtual CameraRole CameraRole { get; set; }

    public int AreaId { get; set; }

    [ForeignKey(nameof(AreaId))]
    public virtual Area Area { get; set; }

    public string? Endpoint { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public bool UseSsl { get; set; }
}
