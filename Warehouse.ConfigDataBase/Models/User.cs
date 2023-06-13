using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.ConfigDataBase.Models;

[Index(nameof(Login))]
public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public int RoleId { get; set; }
}
