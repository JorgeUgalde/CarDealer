using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class AplicationDbContext : DbContext
    {

        // Simpre usar el Dbset para las clases de la BD
        public DbSet<Make> Makes { get; set; }
        public DbSet<VehicleModel> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }



        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
            
        }

    }
}
