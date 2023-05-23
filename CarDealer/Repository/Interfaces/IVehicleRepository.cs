using CarDealer.Models;

namespace CarDealer.Repository.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {

        void Update(Vehicle vehicle);
    }
}
