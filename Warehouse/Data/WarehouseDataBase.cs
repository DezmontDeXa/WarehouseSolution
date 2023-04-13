using Microsoft.EntityFrameworkCore;
using Warehouse.Models;

namespace Warehouse.Data
{
    public class WarehouseDataBase : DbContext
    {
        public DbSet<Car> Cars { get; private set; }
        public DbSet<PlateNumber> PlateNumbers { get; private set; }
        public DbSet<CarStatus> Statuses { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=warehouse;user=DezmontDeXa;password=325325dm");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<CarStatus>(status =>
            {
                status.HasKey(e => e.Id);
                status.Property(e => e.Description).IsRequired();
            });

            modelBuilder.Entity<PlateNumber>(plateNumber =>
            {
                plateNumber.HasKey(e => e.Id);
                plateNumber.Property(e => e.Value).IsRequired();
                plateNumber.HasOne(e => e.Car).WithMany(x => x.PlateNumbers);
                plateNumber.HasMany(e => e.SimilarValues);
            });

            modelBuilder.Entity<Car>(car =>
            {
                car.HasKey(e => e.Id);
                car.HasOne(e => e.CarStatus);
            });
        }
    }
}
