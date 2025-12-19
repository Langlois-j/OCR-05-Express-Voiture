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
        public DbSet<CarBrand> CarBrand { get; set; } = null!;
        public DbSet<CarModel> CarModel { get; set; } = null!;
        public DbSet<RepairType> RepairType { get; set; } = null!;
        public DbSet<Car> Car { get; set; } = null!;
        public DbSet<CarRepair> CarRepair { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed des marques
            builder.Entity<CarBrand>().HasData(
                new { Id = SeedData.SeedBrands.Renault, Name = "Renault" },
                new { Id = SeedData.SeedBrands.Mazda, Name = "Mazda" },
                new { Id = SeedData.SeedBrands.Jeep, Name = "Jeep" },
                new { Id = SeedData.SeedBrands.Ford, Name = "Ford" },
                new { Id = SeedData.SeedBrands.Honda, Name = "Honda" },
                new { Id = SeedData.SeedBrands.Volkswagen, Name = "Volkswagen" }
            );

            // Seed des modèles
            builder.Entity<CarModel>().HasData(
                new { Id = SeedData.SeedModels.Miata, Name = "Miata", BrandId = SeedData.SeedBrands.Mazda },
                new { Id = SeedData.SeedModels.Cx5, Name = "CX-5", BrandId = SeedData.SeedBrands.Mazda },
                new { Id = SeedData.SeedModels.Wrangler, Name = "Wrangler", BrandId = SeedData.SeedBrands.Jeep },
                new { Id = SeedData.SeedModels.Cherokee, Name = "Cherokee", BrandId = SeedData.SeedBrands.Jeep },
                new { Id = SeedData.SeedModels.Mustang, Name = "Mustang", BrandId = SeedData.SeedBrands.Ford },
                new { Id = SeedData.SeedModels.F150, Name = "F-150", BrandId = SeedData.SeedBrands.Ford },
                new { Id = SeedData.SeedModels.Civic, Name = "Civic", BrandId = SeedData.SeedBrands.Honda },
                new { Id = SeedData.SeedModels.Accord, Name = "Accord", BrandId = SeedData.SeedBrands.Honda },
                new { Id = SeedData.SeedModels.Clio, Name = "Clio", BrandId = SeedData.SeedBrands.Renault },
                new { Id = SeedData.SeedModels.Megane, Name = "Megane", BrandId = SeedData.SeedBrands.Renault },
                new { Id = SeedData.SeedModels.Golf, Name = "Golf", BrandId = SeedData.SeedBrands.Volkswagen },
                new { Id = SeedData.SeedModels.Passat, Name = "Passat", BrandId = SeedData.SeedBrands.Volkswagen }
            );
            builder.Entity<RepairType>().HasData(
            new { Id = SeedData.SeedRepairType.RestaurationComplete, Name = "Restauration Complete" },
            new { Id = SeedData.SeedRepairType.RestaurationComplete, Name = "Restauration Complete" },
            new { Id = SeedData.SeedRepairType.RotuleAvant, Name = "Rotule Avant" },
            new { Id = SeedData.SeedRepairType.RotuleArriere, Name = "Rotule Arriere" },
            new { Id = SeedData.SeedRepairType.Radiateur, Name = "Radiateur  " },
            new { Id = SeedData.SeedRepairType.PneusAvant, Name = "Pneus Avant" },
            new { Id = SeedData.SeedRepairType.PneusArriere, Name = "Pneus Arriere" },
            new { Id = SeedData.SeedRepairType.Freins, Name = "Freins" },
            new { Id = SeedData.SeedRepairType.Climatisation, Name = "Climatisation" }

           );
        }
    }
}
