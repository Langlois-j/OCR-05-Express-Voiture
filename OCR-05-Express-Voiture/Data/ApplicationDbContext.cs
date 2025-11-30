using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Models.Entities;
using System;
using System.Reflection.Emit;
namespace OCR_05_Express_Voiture.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet pour les classes
        public DbSet<CarBrand> CarBrands { get; set; } = null!;
        public DbSet<CarModel> CarModels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // GUID fixes pour les marques
            var renaultId    = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000001");
            var mazdaId      = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000002");
            var jeepId       = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000003");
            var fordId       = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000004");
            var hondaId      = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000005");
            var volkswagenId = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-000000000006");

            // GUID fixes pour les modèles
            var miataId     = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000001");
            var cx5Id       = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000002");
            var wranglerId  = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000003");
            var cherokeeId  = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000004");
            var mustangId   = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000005");
            var f150Id      = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000006");
            var civicId     = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000007");
            var accordId    = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000008");
            var clioId      = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000009");
            var meganeId    = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000010");
            var golfId      = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000011");
            var passatId    = Guid.Parse("31c9b0b6-1a2d-4f61-8fcm-000000000012");

            // Seed des marques
            builder.Entity<CarBrand>().HasData(
                new { Id = renaultId,    Name = "Renault" },
                new { Id = mazdaId,      Name = "Mazda" },
                new { Id = jeepId,       Name = "Jeep" },
                new { Id = fordId,       Name = "Ford" },
                new { Id = hondaId,      Name = "Honda" },
                new { Id = volkswagenId, Name = "Volkswagen" }
            );

            // Seed des modèles
            builder.Entity<CarModel>().HasData(
                new { Id = miataId, Name = "Miata", BrandId = mazdaId },
                new { Id = cx5Id, Name = "CX-5", BrandId = mazdaId },
                new { Id = wranglerId, Name = "Wrangler", BrandId = jeepId },
                new { Id = cherokeeId, Name = "Cherokee", BrandId = jeepId },
                new { Id = mustangId, Name = "Mustang", BrandId = fordId },
                new { Id = f150Id, Name = "F-150", BrandId = fordId },
                new { Id = civicId, Name = "Civic", BrandId = hondaId },
                new { Id = accordId, Name = "Accord", BrandId = hondaId },
                new { Id = clioId, Name = "Clio", BrandId = renaultId },
                new { Id = meganeId, Name = "Megane", BrandId = renaultId },
                new { Id = golfId, Name = "Golf", BrandId = volkswagenId },
                new { Id = passatId, Name = "Passat", BrandId = volkswagenId }
            );
        }
    }
}
