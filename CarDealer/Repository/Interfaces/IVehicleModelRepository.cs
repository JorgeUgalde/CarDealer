using CarDealer.Models;

namespace CarDealer.Repository.Interfaces
{
    public interface IVehicleModelRepository : IRepository<VehicleModel>
    {
        void Update(VehicleModel make);
    }
}
