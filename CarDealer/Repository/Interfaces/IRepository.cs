using System.Linq.Expressions;

namespace CarDealer.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();


        // Va a traer una expresion, con esta forma, un valor de retorno y el nombre que se llama filtro
        T Get(Expression<Func<T, bool>> Filter);
        void add(T entity);

        void remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
