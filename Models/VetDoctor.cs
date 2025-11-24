using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models
{
    public class VetDoctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}

