


using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using System.Linq.Expressions;

namespace CarDealer.Repository
{
    public class MakeRepository : Repository<Make> ,IMakeRepository
    {

        private AplicationDbContext _db;


        //Asi corrije el error del DB, se envia a la clase base el _db
        public MakeRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Make obj)
        {
            _db.Makes.Update(obj);
        }

    }



}
