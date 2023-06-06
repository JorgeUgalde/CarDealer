using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace CarDealer.Models
{
    public class ApplicationUser :IdentityUser

    {

        [Required]
        public String Name { get; set; }

        public string? StreetAddress { get; set; }



    }
}
