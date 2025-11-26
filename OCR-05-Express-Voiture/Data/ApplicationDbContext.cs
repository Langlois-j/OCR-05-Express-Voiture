using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet pour les marques de voiture
        public DbSet<CarBrand> CarBrands { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed de données (GUIDs fixes pour la cohérence)
            builder.Entity<CarBrand>().HasData(
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000001"), Name = "Renault" },
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000002"), Name = "Mazda" },
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000003"), Name = "Jeep" },
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000004"), Name = "Ford" },
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000005"), Name = "Honda" },
                new { Id = Guid.Parse("21c9b0b6-1a2d-4f61-8f1d-000000000006"), Name = "Volkswagen" }
            );


;        }
    }
}
