
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;

namespace CarDealer.Repository
{
    public class VehicleRepository : Repository<Vehicle> ,IVehicleRepository
    {
        private AplicationDbContext _db;


        //Asi corrije el error del DB, se envia a la clase base el _db
        public VehicleRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Vehicle obj)
        {
            _db.Vehicles.Update(obj);
        }
    }
}

