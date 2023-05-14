using CarDealer.Data;
using CarDealer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;




namespace CarDealer.Repository

{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AplicationDbContext _db;
        internal DbSet<T> dbSet;

        //Constructor 
        public Repository(AplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void add(T entity)
        {
            dbSet.Add(entity);
        }

        //Cuando recibo una expresion para buscar segun se requiera y se retorne por el filtro buscado
        public T Get(Expression<Func<T, bool>> Filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(Filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }


    }
}
