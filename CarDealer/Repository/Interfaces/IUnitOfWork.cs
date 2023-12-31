﻿namespace CarDealer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IMakeRepository Make { get; }
        IVehicleModelRepository VehicleModel { get; }

        IVehicleRepository Vehicle { get; }
        object VehicleVM { get; set; }

        void Save();

    }
}
