using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class PetProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }

        [Display(Name = "Veterinary Notes")]
        public string? VetNotes { get; set; }

        // Navigation property
        [ForeignKey("PetId")]
        public virtual Pet? Pet { get; set; }
    }
}

