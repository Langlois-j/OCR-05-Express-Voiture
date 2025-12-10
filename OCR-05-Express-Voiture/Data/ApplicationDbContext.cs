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
        public DbSet<Repair> Repair { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var passatId = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-000000000062");

            // Seed des marques
            builder.Entity<CarBrand>().HasData(
                new { Id = SeedData.Brands.Renault, Name = "Renault" },
                new { Id = SeedData.Brands.Mazda, Name = "Mazda" },
                new { Id = SeedData.Brands.Jeep, Name = "Jeep" },
                new { Id = SeedData.Brands.Ford, Name = "Ford" },
                new { Id = SeedData.Brands.Honda, Name = "Honda" },
                new { Id = SeedData.Brands.Volkswagen, Name = "Volkswagen" }
            );

            // Seed des modèles
            builder.Entity<CarModel>().HasData(
                new { Id = SeedData.Models.Miata, Name = "Miata", BrandId = SeedData.Brands.Mazda },
                new { Id = SeedData.Models.Cx5, Name = "CX-5", BrandId = SeedData.Brands.Mazda },
                new { Id = SeedData.Models.Wrangler, Name = "Wrangler", BrandId = SeedData.Brands.Jeep },
                new { Id = SeedData.Models.Cherokee, Name = "Cherokee", BrandId = SeedData.Brands.Jeep },
                new { Id = SeedData.Models.Mustang, Name = "Mustang", BrandId = SeedData.Brands.Ford },
                new { Id = SeedData.Models.F150, Name = "F-150", BrandId = SeedData.Brands.Ford },
                new { Id = SeedData.Models.Civic, Name = "Civic", BrandId = SeedData.Brands.Honda },
                new { Id = SeedData.Models.Accord, Name = "Accord", BrandId = SeedData.Brands.Honda },
                new { Id = SeedData.Models.Clio, Name = "Clio", BrandId = SeedData.Brands.Renault },
                new { Id = SeedData.Models.Megane, Name = "Megane", BrandId = SeedData.Brands.Renault },
                new { Id = SeedData.Models.Golf, Name = "Golf", BrandId = SeedData.Brands.Volkswagen },
                new { Id = SeedData.Models.Passat, Name = "Passat", BrandId = SeedData.Brands.Volkswagen }
            );
            //            builder.Entity<RepairType>().HasData(
            //            new { Id = SeedData.RepairType.RestaurationComplete ,Name = "Restauration Complete"} ,
            //            new { Id = SeedData.RepairType.RestaurationComplete ,Name = "Restauration Complete"} ,
            //            new { Id = SeedData.RepairType.RotuleAvant          ,Name ="Rotule Avant"          } ,
            //            new { Id = SeedData.RepairType.RotuleArriere        ,Name ="Rotule Arriere"        } ,
            //                new { Id = SeedData.RepairType.Radiateur            ,Name ="Radiateur  "           } ,
            //           new { Id = SeedData.RepairType.PneusAvant           ,Name ="Pneus Avant"           } ,
            //              new { Id = SeedData.RepairType.PneusArriere         ,Name ="Pneus Arriere"         } ,
            //          new { Id = SeedData.RepairType.Freins               ,Name ="Freins"                } ,
            //          new { Id = SeedData.RepairType.Climatisation, Name = "Climatisation"               }
            //          
            //           );
        }
    }
}
