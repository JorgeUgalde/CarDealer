


using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using System.Linq.Expressions;

namespace CarDealer.Repository
{
    public class VehicleModelRepository : Repository<VehicleModel> ,IVehicleModelRepository
    {

        private AplicationDbContext _db;


        //Asi corrije el error del DB, se envia a la clase base el _db
        public VehicleModelRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(VehicleModel obj)
        {
            _db.Models.Update(obj);
        }

    }



}
