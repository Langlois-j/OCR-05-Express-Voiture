using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using OCR_05_Express_Voiture_Test.configuratiion;

namespace OCR_05_Express_Voiture_Test.Integration
{

    public class CarControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;


        public CarControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        /// <summary>
        ///Test d'ajout 
        /// </summary>
        public async Task AddCarToDatabase()
        {
            // ARRANGE

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var newCar = new Car
            {
                VinCode = "TEST123456789ABCD",
                BrandId = 1,
                ModelId = 1,
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
            Assert.Equal(1, carInDb.BrandId);
            Assert.Equal(1, carInDb.ModelId);
            Assert.Equal(25000, carInDb.Mileage);
            Assert.True(carInDb.ForSell);
            Assert.False(carInDb.Sold);
        }
        [Fact]
        /// <summary>
        ///Test de modification
        /// </summary>
        public async Task EditCarInDatabase()
        {
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            var carToEdit = new Car
            {
                VinCode = "EDIT-TEST-CAR-001",
                BrandId = 1,
                ModelId = 1,
                TrimLevel = "Sport",
                ConstructionYear = 2020,
                Mileage = 50000,
                ForSell = true,
                Sold = false,
                RepairAmount = 1000.00,
                RepairDescription = "État moyen"
            };

            context.Car.Add(carToEdit);
            await context.SaveChangesAsync();


            var carFromDb = await context.Car
                .FirstOrDefaultAsync(c => c.VinCode == "EDIT-TEST-CAR-001");

            Assert.NotNull(carFromDb);
            var originalId = carFromDb.Id;

            // ACT - Modifier la voiture
            carFromDb.Mileage = 75000;
            carFromDb.BrandId = 2;
            carFromDb.ModelId = 3;
            carFromDb.RepairAmount = 1500.00;
            carFromDb.TrimLevel = "Luxe";
            carFromDb.RepairDescription = "Texte Changer";
            carFromDb.Sold = true;
            carFromDb.ForSell = false;


            context.Car.Update(carFromDb);
            await context.SaveChangesAsync();

            // ASSERT - Vérifier que les modifications ont été sauvegardées
            var updatedCarInDb = await context.Car
                .FirstOrDefaultAsync(c => c.Id == originalId);

            Assert.NotNull(updatedCarInDb);
            Assert.Equal(originalId, updatedCarInDb.Id);

            Assert.Equal(carFromDb.Mileage, updatedCarInDb.Mileage);
            Assert.Equal(carFromDb.BrandId, updatedCarInDb.BrandId);
            Assert.Equal(carFromDb.ModelId, updatedCarInDb.ModelId);
            Assert.Equal(carFromDb.RepairAmount, updatedCarInDb.RepairAmount);
            Assert.Equal(carFromDb.TrimLevel, updatedCarInDb.TrimLevel);
            Assert.Equal(carFromDb.RepairDescription, updatedCarInDb.RepairDescription);
            Assert.Equal(carFromDb.Sold, updatedCarInDb.Sold);
            Assert.Equal(carFromDb.ForSell, updatedCarInDb.ForSell);

        }
        [Fact]
        /// <summary>
        /// Test de suppression d'une voiture
        /// </summary>
        public async Task DeleteCarFromDatabase()
        {
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Créer une voiture de test
            var carToDelete = new Car
            {
                VinCode = "DELETE-TEST-CAR-001",
                BrandId = 1,
                ModelId = 1,
                TrimLevel = "Basic",
                ConstructionYear = 2019,
                Mileage = 100000,
                ForSell = false,
                Sold = true,
                RepairAmount = 500.00,
                RepairDescription = "À enlever"
            };

            context.Car.Add(carToDelete);
            await context.SaveChangesAsync();

            var carId = carToDelete.Id;

            // ACT - Supprimer la voiture
            var carFromDb = await context.Car.FindAsync(carId);
            Assert.NotNull(carFromDb);

            context.Car.Remove(carFromDb);
            await context.SaveChangesAsync();

            // ASSERT 
            var deletedCar = await context.Car
                .FirstOrDefaultAsync(c => c.Id == carId);

            Assert.Null(deletedCar);
        }
    }
}