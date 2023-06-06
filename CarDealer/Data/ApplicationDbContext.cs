using CarDealer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Make> Makes { get; set; }
        public DbSet<VehicleModel> Models { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
