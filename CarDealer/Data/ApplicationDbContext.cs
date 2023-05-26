using CarDealer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Make> Makes { get; set; }
        public DbSet<VehicleModel> Models { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
