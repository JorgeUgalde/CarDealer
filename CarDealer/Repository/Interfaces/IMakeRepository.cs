using CarDealer.Models;

namespace CarDealer.Repository.Interfaces
{
    public interface IMakeRepository : IRepository<Make>
    {
        void Update(Make make);
    }
}
