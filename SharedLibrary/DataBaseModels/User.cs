using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SharedLibrary.DataBaseModels;

[Index(nameof(Login))]
public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public virtual UserRole Role { get; set; }
}

public partial class UserRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string RoleName { get; set; }

    public Permissions Permissions { get; set; }

}

[Flags]
public enum Permissions
{
    None = 0,
    All = 1024,
    
}
