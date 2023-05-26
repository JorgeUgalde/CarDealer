using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Make
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Make Name")]
        public string Name { get; set; }

    }

    
}
