using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace OCR_05_Express_Voiture_Test.Integration
{
    /// <summary>
    /// Classe qui simule l'application web pour les tests
    /// Elle crée une base de données en mémoire (temporaire)
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory <Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // 1. Retirer le DbContext existant (celui qui pointe vers SQL Server)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // 2. Ajouter un DbContext InMemory pour les tests
                // InMemory = base de données temporaire en mémoire RAM
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });

                // 3. Créer la base de données et la remplir avec des données de test
                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated(); // Créer la structure

                    // Remplir avec des données de test
                    SeedTestData(db);
                }
            });
        }

        /// <summary>
        /// Remplir la base de données avec des données de test
        /// </summary>
        private void SeedTestData(ApplicationDbContext context)
        {
            // Nettoyer d'abord (au cas où)
            context.Car.RemoveRange(context.Car);
            context.CarModel.RemoveRange(context.CarModel);
            context.CarBrand.RemoveRange(context.CarBrand);
            context.SaveChanges();

            // Ajouter des marques de test
            var brands = new[]
            {
                new CarBrand { Id = 1, Name = "C1" },
                new CarBrand { Id = 2, Name = "C2" },
                new CarBrand { Id = 3, Name = "C3" }
            };
            context.CarBrand.AddRange(brands);
            context.SaveChanges();

            // Ajouter des modèles de test
            var models = new[]
            {
                new CarModel { Id = 1, Name = "C1M1", CarBrandId = 1 },
                new CarModel { Id = 2, Name = "C1M2", CarBrandId = 1 },
                new CarModel { Id = 3, Name = "C2M1", CarBrandId = 2 },
                new CarModel { Id = 4, Name = "C2M2", CarBrandId = 2 },
                new CarModel { Id = 5, Name = "C3M1", CarBrandId = 3 }
            };
            context.CarModel.AddRange(models);
            context.SaveChanges();
        }
    }
}