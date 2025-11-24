using VeterinaryClinic.Models;

namespace VeterinaryClinic.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<VeterinaryClinicDbContext>();

            // Check if data already exists
            if (context.VetDoctors.Any())
            {
                return; // Database has been seeded
            }

            // Seed VetDoctors
            var doctors = new List<VetDoctor>
            {
                new VetDoctor { Name = "Dr. Sarah Johnson", Specialty = "Small Animal Medicine" },
                new VetDoctor { Name = "Dr. Michael Chen", Specialty = "Surgery" },
                new VetDoctor { Name = "Dr. Emily Rodriguez", Specialty = "Exotic Animals" }
            };

            context.VetDoctors.AddRange(doctors);
            context.SaveChanges();

            // Seed Pets
            var pets = new List<Pet>
            {
                new Pet { MicrochipId = "MC001", Name = "Buddy", Species = "Dog", VetDoctorId = doctors[0].Id },
                new Pet { MicrochipId = "MC002", Name = "Whiskers", Species = "Cat", VetDoctorId = doctors[0].Id },
                new Pet { MicrochipId = "MC003", Name = "Max", Species = "Dog", VetDoctorId = doctors[1].Id },
                new Pet { MicrochipId = "MC004", Name = "Luna", Species = "Rabbit", VetDoctorId = doctors[2].Id }
            };

            context.Pets.AddRange(pets);
            context.SaveChanges();

            // Seed PetProfiles
            var profiles = new List<PetProfile>
            {
                new PetProfile { PetId = pets[0].Id, VetNotes = "Regular checkup completed. Healthy weight maintained. Next vaccination due in 6 months." },
                new PetProfile { PetId = pets[1].Id, VetNotes = "Annual examination done. Dental cleaning recommended. Overall health is good." },
                new PetProfile { PetId = pets[2].Id, VetNotes = "Post-surgery follow-up. Recovery progressing well. Stitches removed successfully." },
                new PetProfile { PetId = pets[3].Id, VetNotes = "First visit completed. Diet consultation provided. Owner advised on proper care for exotic pet." }
            };

            context.PetProfiles.AddRange(profiles);
            context.SaveChanges();
        }
    }
}

