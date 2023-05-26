using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Models.ViewModels
{
    public class VehicleModelVM
    {
        [ValidateNever]
        public VehicleModel VehicleModel { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> MakeList { get; set; }
    }
}
