using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text;

namespace SharedLibrary.DataBaseModels;

public partial class Camera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Link { get; set; }

    public MoveDirection Direction { get; set; }

    public int RoleId { get; set; }


    [ForeignKey(nameof(RoleId))]
    public virtual CameraRole CameraRole { get; set; }

    public int AreaId { get; set; }

    [ForeignKey(nameof(AreaId))]
    public virtual Area Area { get; set; }
}

public enum MoveDirection
{
    Forward = 0, Backward = 1,
    Both = 2
}