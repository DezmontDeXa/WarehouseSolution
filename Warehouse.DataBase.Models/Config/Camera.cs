﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.DataBase.Models.Main;
using Warehouse.Interfaces.DataBase;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.DataBase.Models.Config;

public partial class Camera : ICamera
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Link { get; set; }

    public MoveDirection Direction { get; set; }

    public int RoleId { get; set; }

    public int AreaId { get; set; }
}
