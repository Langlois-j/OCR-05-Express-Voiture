using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using Xunit;

namespace OCR_05_Express_Voiture_Test.Integration
{
    /// <summary>
    /// Test simple pour vérifier qu'on peut ajouter une voiture dans la base de données
    /// </summary>
    public class CarControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

       
        public CarControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact] // 
        public async Task CanAddCarToDatabase()
        {
            // ARRANGE
            // On récupère la base de données de test
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // On crée une nouvelle voiture
            var newCar = new Car
            {
                VinCode = "TEST123456789ABCD",
                CarBrandId = 1,     
                CarModelId = 1,     
                TrimLevel = "Zen",
                ConstructionYear = 2021,
                Mileage = 25000,
                ForSell = true,
                Sold = false,
                RepairAmount = 500.00,
                RepairDescription = "Révision 25000 km"
            };

            // ACT 
            context.Car.Add(newCar);
            await context.SaveChangesAsync();

            // ASSERT 
           
            var carInDb = await context.Car
                .FirstOrDefaultAsync(c => c.VinCode == "TEST123456789ABCD");

            // Vérifications
            Assert.NotNull(carInDb);                             
            Assert.Equal("TEST123456789ABCD", carInDb.VinCode);  
            Assert.Equal(1, carInDb.CarBrandId);                 
            Assert.Equal(1, carInDb.CarModelId);                 
            Assert.Equal(25000, carInDb.Mileage);                
            Assert.True(carInDb.ForSell);                        
            Assert.False(carInDb.Sold);                          
        }
    }
}