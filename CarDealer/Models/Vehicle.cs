using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NPOI.SS.Formula.Functions;

namespace CarDealer.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("VehicleModelID")]
        public VehicleModel Model { get; set; }


        [Required]
        [CurrentYearValidation]
        public int Year { get; set; }


        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string PictureURL { get; set; }


        [Required]
        [Display(Name = "ModelID")]
        public int VehicleModelID { get; set; }

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

