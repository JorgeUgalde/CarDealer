using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using System.Linq.Expressions;

namespace CarDealer.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {

        private ApplicationDbContext _db;

        public VehicleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Vehicle obj)
        {
            _db.Vehicles.Update(obj);
        }
    }
}
