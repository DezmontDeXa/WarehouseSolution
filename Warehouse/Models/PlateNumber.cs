namespace Warehouse.Models
{
    public class PlateNumber
    {
        public int Id { get; set; }
        public virtual Car Car { get; set; }
        public string Value { get; set; }
        public virtual ICollection<PlateNumber> SimilarValues { get; set; }
    }
}
