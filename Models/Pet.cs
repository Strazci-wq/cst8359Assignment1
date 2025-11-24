using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeterinaryClinic.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Microchip ID")]
        public string MicrochipId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Species { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Veterinarian")]
        public int VetDoctorId { get; set; }

        // Navigation properties
        [ForeignKey("VetDoctorId")]
        public virtual VetDoctor? VetDoctor { get; set; }

        public virtual PetProfile? PetProfile { get; set; }
    }
}

