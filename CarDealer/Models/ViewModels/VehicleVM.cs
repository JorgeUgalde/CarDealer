using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealer.Models.ViewModels
{
    public class VehicleVM
    {
        [ValidateNever]
        public Vehicle Vehicle { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> VehicleModelList { get; set; }
    }
}
