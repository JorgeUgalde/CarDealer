using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using System.Linq.Expressions;

namespace CarDealer.Repository
{
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {

        private ApplicationDbContext _db;

        public VehicleModelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(VehicleModel obj)
        {
            _db.Models.Update(obj);
        }
    }
}
