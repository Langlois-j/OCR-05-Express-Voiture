using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture_Test.configuratiion
{
    /// <summary>
    /// Classe qui simule l'application web pour les tests
    /// Elle crée une base de données en mémoire (temporaire)
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // ✅ Base de données UNIQUE par instance
                var databaseName = $"TestDatabase_{Guid.NewGuid()}";

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName);
                });

                var serviceProvider = services.BuildServiceProvider();
                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Database.EnsureCreated();
                    SeedTestData(db);
                }
            });
        }

        /// <summary>
        /// Remplir la base de données avec des données de test
        /// </summary>
        public static void SeedTestData(ApplicationDbContext context)
        {

            var brands = new[]
            {
                new CarBrand { Name = "C1" },  
                new CarBrand { Name = "C2" }, 
                new CarBrand { Name = "C3" }  
            };
            context.CarBrand.AddRange(brands);
            context.SaveChanges();

            
            var brand1 = context.CarBrand.First(b => b.Name == "C1");
            var brand2 = context.CarBrand.First(b => b.Name == "C2");
            var brand3 = context.CarBrand.First(b => b.Name == "C3");

           
            var models = new[]
            {
                new CarModel { Name = "C1M1", CarBrandId = brand1.Id },
                new CarModel { Name = "C1M2", CarBrandId = brand1.Id },
                new CarModel { Name = "C2M1", CarBrandId = brand2.Id },
                new CarModel { Name = "C2M2", CarBrandId = brand2.Id },
                new CarModel { Name = "C3M1", CarBrandId = brand3.Id }
            };
            context.CarModel.AddRange(models);
            context.SaveChanges();
        }
    }
}
