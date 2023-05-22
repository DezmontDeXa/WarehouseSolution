using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.DataBaseModels;

public partial class NotInListCarNotify
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string DetectedPlateNumber { get; set; }

    public int Direction { get; set; }

    public byte[] PlateNumberPicture { get; set; }

    public virtual Car? Car { get; set; }

    public virtual Camera? Camera { get; set; }

    public virtual CameraRole? Role { get; set; }
}
