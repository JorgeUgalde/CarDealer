using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Models
{
    public class Make
    {
        [Key] //llave primaria a algun Id que se llame de otra forma
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        [DisplayName("Make Name")]
        public string Name { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }

}