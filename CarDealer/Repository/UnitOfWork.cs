using CarDealer.Data;
using CarDealer.Repository.Interfaces;

namespace CarDealer.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AplicationDbContext _db;



        public IMakeRepository Make { get; private set; }


        //Asi corrije el error del DB, se envia a la clase base el _db
        public UnitOfWork(AplicationDbContext db) 
        {
            _db = db;
            Make = new MakeRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }



    }
}
