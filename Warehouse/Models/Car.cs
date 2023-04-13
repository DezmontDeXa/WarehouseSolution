namespace Warehouse.Models
{
    public class Car
    {
        public int Id { get; set; }
        public virtual CarStatus CarStatus { get; set; }
        public virtual ICollection<PlateNumber> PlateNumbers { get; set; }
    }
}
