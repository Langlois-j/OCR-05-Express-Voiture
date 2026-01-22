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

        // DbSet pour les classes
        public DbSet<CarBrand> CarBrand { get; set; } = null!;
        public DbSet<CarModel> CarModel { get; set; } = null!;

        public DbSet<Car> Car { get; set; } = null!;






        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>()
        .HasOne(c => c.Brand)
        .WithMany()
        .HasForeignKey(c => c.BrandId)
        .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany()
                .HasForeignKey(c => c.ModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CarModel>()
                .HasOne(c => c.CarBrand)
                .WithMany()
                .HasForeignKey(c => c.CarBrandId)
                .OnDelete(DeleteBehavior.Restrict);



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
                new { Id = SeedData.SeedModels.Miata, Name = "Miata", CarBrandId = SeedData.SeedBrands.Mazda },
                new { Id = SeedData.SeedModels.Cx5, Name = "CX-5", CarBrandId = SeedData.SeedBrands.Mazda },
                new { Id = SeedData.SeedModels.Wrangler, Name = "Wrangler", CarBrandId = SeedData.SeedBrands.Jeep },
                new { Id = SeedData.SeedModels.Cherokee, Name = "Cherokee", CarBrandId = SeedData.SeedBrands.Jeep },
                new { Id = SeedData.SeedModels.Mustang, Name = "Mustang", CarBrandId = SeedData.SeedBrands.Ford },
                new { Id = SeedData.SeedModels.F150, Name = "F-150", CarBrandId = SeedData.SeedBrands.Ford },
                new { Id = SeedData.SeedModels.Civic, Name = "Civic", CarBrandId = SeedData.SeedBrands.Honda },
                new { Id = SeedData.SeedModels.Accord, Name = "Accord", CarBrandId = SeedData.SeedBrands.Honda },
                new { Id = SeedData.SeedModels.Clio, Name = "Clio", CarBrandId = SeedData.SeedBrands.Renault },
                new { Id = SeedData.SeedModels.Megane, Name = "Megane", CarBrandId = SeedData.SeedBrands.Renault },
                new { Id = SeedData.SeedModels.Golf, Name = "Golf", CarBrandId = SeedData.SeedBrands.Volkswagen },
                new { Id = SeedData.SeedModels.Passat, Name = "Passat", CarBrandId = SeedData.SeedBrands.Volkswagen }
            );

        }
    }
}
