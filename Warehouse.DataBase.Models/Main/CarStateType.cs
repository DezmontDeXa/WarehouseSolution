using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Interfaces.DataBase;

namespace Warehouse.DataBase.Models.Main
{
    public partial class CarStateType : ICarStateType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string TypeName { get; set; } = null!;
    }
}