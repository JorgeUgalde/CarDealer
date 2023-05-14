using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Model Name")]
        public string Name { get; set; }

        public int MakeID { get; set;}

        [ForeignKey("MakeID")]
        public Make Make { get; set;}
               
    }
}
