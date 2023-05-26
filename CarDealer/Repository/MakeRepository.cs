using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Repository.Interfaces;
using System.Linq.Expressions;

namespace CarDealer.Repository
{
    public class MakeRepository : Repository<Make>, IMakeRepository
    {

        private ApplicationDbContext _db;

        public MakeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Make obj)
        {
            _db.Makes.Update(obj);
        }
    }
}
