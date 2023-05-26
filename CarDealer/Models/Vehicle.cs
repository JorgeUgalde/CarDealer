//using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Vehicle
    {
        public int Id { get; set; }


        public int ModelId { get; set; }

        [Required]
        [ForeignKey("ModelId")]
        public VehicleModel Model { get; set; }

        [Required]
        [CurrentYearValidation]
        public int Year { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }


        public string PictureUrl { get; set; }

    }

    class CurrentYearValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((int)value >= 1950 && (int)value <= DateTime.Now.Year + 1)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Year must be between 1950 and the current year +1");
        }
    }

}
