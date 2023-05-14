namespace CarDealer.Models.ViewModels
{
    public class VehicleModelVM
    {
        public VehicleModel VehicleModel { get; set; }

        public IEnumerable<Make> MakeList { get; set; }
    }
}
