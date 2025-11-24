using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Data
{
    public class VeterinaryClinicDbContext : DbContext
    {
        public VeterinaryClinicDbContext(DbContextOptions<VeterinaryClinicDbContext> options)
            : base(options)
        {
        }

        public DbSet<VetDoctor> VetDoctors { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetProfile> PetProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure VetDoctor -> Pet relationship (one-to-many)
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.VetDoctor)
                .WithMany(v => v.Pets)
                .HasForeignKey(p => p.VetDoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Pet -> PetProfile relationship (one-to-one)
            modelBuilder.Entity<PetProfile>()
                .HasOne(pp => pp.Pet)
                .WithOne(p => p.PetProfile)
                .HasForeignKey<PetProfile>(pp => pp.PetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

