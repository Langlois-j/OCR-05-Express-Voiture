using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using OCR_05_Express_Voiture_Test.configuratiion;

namespace OCR_05_Express_Voiture_Test.Integration
{
    /// <summary>
    /// Tests d'intégration complets pour le contrôleur Cars

    /// </summary>
    public class CarControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CarControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }
        private async Task ResetDatabaseAsync()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            CustomWebApplicationFactory.SeedTestData(context);
        }



        [Fact]
        /// <summary>
        /// Test d'ajout d'une voiture
        /// </summary>
        public async Task AddCarToDatabase()
        {
            // ARRANGE
            await ResetDatabaseAsync();
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
                SellPrice = 500.00,
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
        /// Test de modification d'une voiture
        /// </summary>
        public async Task EditCarInDatabase()
        {
            // ARRANGE
            await ResetDatabaseAsync();
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
                SellPrice = 1000.00,
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
            carFromDb.SellPrice = 1500.00;
            carFromDb.TrimLevel = "Luxe";
            carFromDb.RepairDescription = "Texte Changé";
            carFromDb.Sold = true;
            carFromDb.ForSell = false;

            context.Car.Update(carFromDb);
            await context.SaveChangesAsync();

            // ASSERT - Vérifier que les modifications ont été sauvegardées
            var updatedCarInDb = await context.Car
                .FirstOrDefaultAsync(c => c.Id == originalId);

            Assert.NotNull(updatedCarInDb);
            Assert.Equal(originalId, updatedCarInDb.Id);
            Assert.Equal(75000, updatedCarInDb.Mileage);
            Assert.Equal(2, updatedCarInDb.BrandId);
            Assert.Equal(3, updatedCarInDb.ModelId);
            Assert.Equal(1500.00, updatedCarInDb.SellPrice);
            Assert.Equal("Luxe", updatedCarInDb.TrimLevel);
            Assert.Equal("Texte Changé", updatedCarInDb.RepairDescription);
            Assert.True(updatedCarInDb.Sold);
            Assert.False(updatedCarInDb.ForSell);
        }

        [Fact]
        /// <summary>
        /// Test de suppression d'une voiture
        /// </summary>
        public async Task DeleteCarFromDatabase()
        {
            await ResetDatabaseAsync();
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
                SellPrice = 500.00,
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



        [Fact]
        public async Task GetAllCars_ShouldReturnAllCars()
        {
            // ARRANGE
            await ResetDatabaseAsync();
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ajouter plusieurs voitures
            var cars = new List<Car>
            {
                new Car { VinCode = "CAR001", BrandId = 1, ModelId = 1, ConstructionYear = 2020, Mileage = 10000, SellPrice = 500 },
                new Car { VinCode = "CAR002", BrandId = 1, ModelId = 2, ConstructionYear = 2021, Mileage = 5000, SellPrice = 300 },
                new Car { VinCode = "CAR003", BrandId = 2, ModelId = 3, ConstructionYear = 2019, Mileage = 50000, SellPrice = 1500 }
            };

            context.Car.AddRange(cars);
            await context.SaveChangesAsync();

            // ACT
            var allCars = await context.Car.ToListAsync();

            // ASSERT
            Assert.True(allCars.Count >= 3);
            Assert.Contains(allCars, c => c.VinCode == "CAR001");
            Assert.Contains(allCars, c => c.VinCode == "CAR002");
            Assert.Contains(allCars, c => c.VinCode == "CAR003");
        }

        [Fact]
        public async Task GetCarById_ShouldReturnCorrectCar()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var car = new Car
            {
                VinCode = "SPECIFIC-CAR-001",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2022,
                Mileage = 15000,
                SellPrice = 800,
                TrimLevel = "Sport"
            };

            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ACT
            var foundCar = await context.Car
                .FirstOrDefaultAsync(c => c.Id == car.Id);

            // ASSERT
            Assert.NotNull(foundCar);
            Assert.Equal("SPECIFIC-CAR-001", foundCar.VinCode);
            Assert.Equal("Sport", foundCar.TrimLevel);
            Assert.Equal(15000, foundCar.Mileage);
        }

        [Fact]
        public async Task GetCarWithBrandAndModel_ShouldIncludeRelations()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var brand = await context.CarBrand.FirstAsync(b => b.Name == "C1");
            var model = await context.CarModel.FirstAsync(m => m.Name == "C1M1");
            var car = new Car
            {
                VinCode = "CAR-WITH-RELATIONS",
                BrandId = brand.Id,
                ModelId = model.Id,
                ConstructionYear = 2020,
                Mileage = 30000,
                SellPrice = 1200
            };

            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ACT
            var carWithRelations = await context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(c => c.VinCode == "CAR-WITH-RELATIONS");

            // ASSERT
            Assert.NotNull(carWithRelations);
            Assert.NotNull(carWithRelations.Brand);
            Assert.NotNull(carWithRelations.Model);
            Assert.Equal("C1", carWithRelations.Brand.Name);
            Assert.Equal("C1M1", carWithRelations.Model.Name);
        }



        [Fact]
        public async Task UpdateCarMileage_ShouldPersist()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var car = new Car
            {
                VinCode = "MILEAGE-UPDATE-CAR",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 10000,
                SellPrice = 500
            };

            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ACT
            var carToUpdate = await context.Car.FindAsync(car.Id);
            Assert.NotNull(carToUpdate);

            carToUpdate.Mileage = 25000;
            context.Update(carToUpdate);
            await context.SaveChangesAsync();

            // ASSERT
            var updatedCar = await context.Car.FindAsync(car.Id);
            Assert.NotNull(updatedCar);
            Assert.Equal(25000, updatedCar.Mileage);
        }

        [Fact]
        public async Task MarkCarAsSold_ShouldUpdateFlags()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var car = new Car
            {
                VinCode = "CAR-TO-SELL",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2021,
                Mileage = 5000,
                SellPrice = 300,
                ForSell = true,
                Sold = false
            };

            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ACT - Marquer comme vendue
            var carToSell = await context.Car.FindAsync(car.Id);
            Assert.NotNull(carToSell);

            carToSell.Sold = true;
            carToSell.ForSell = false;
            context.Update(carToSell);
            await context.SaveChangesAsync();

            // ASSERT
            var soldCar = await context.Car.FindAsync(car.Id);
            Assert.NotNull(soldCar);
            Assert.True(soldCar.Sold);
            Assert.False(soldCar.ForSell);
        }



        [Fact]
        public async Task FilterCarsByBrand_ShouldReturnOnlyMatchingCars()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var cars = new List<Car>
            {
                new Car { VinCode = "BRAND1-CAR1", BrandId = 1, ModelId = 1, ConstructionYear = 2020, Mileage = 10000, SellPrice = 500 },
                new Car { VinCode = "BRAND1-CAR2", BrandId = 1, ModelId = 2, ConstructionYear = 2021, Mileage = 5000, SellPrice = 300 },
                new Car { VinCode = "BRAND2-CAR1", BrandId = 2, ModelId = 3, ConstructionYear = 2019, Mileage = 50000, SellPrice = 1500 }
            };

            context.Car.AddRange(cars);
            await context.SaveChangesAsync();

            // ACT
            var brand1Cars = await context.Car
                .Where(c => c.BrandId == 1)
                .ToListAsync();

            // ASSERT
            Assert.Equal(2, brand1Cars.Count);
            Assert.All(brand1Cars, c => Assert.Equal(1, c.BrandId));
        }

        [Fact]
        public async Task FilterCarsForSale_ShouldReturnOnlyAvailableCars()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var cars = new List<Car>
            {
                new Car { VinCode = "FOR-SALE-1", BrandId = 1, ModelId = 1, ConstructionYear = 2020, Mileage = 10000, SellPrice = 500, ForSell = true, Sold = false },
                new Car { VinCode = "FOR-SALE-2", BrandId = 1, ModelId = 2, ConstructionYear = 2021, Mileage = 5000,  SellPrice = 300, ForSell = true, Sold = false },
                new Car { VinCode = "SOLD-CAR", BrandId = 2, ModelId = 3, ConstructionYear = 2019, Mileage = 50000,   SellPrice = 1500, ForSell = false, Sold = true }
            };

            context.Car.AddRange(cars);
            await context.SaveChangesAsync();

            // ACT
            var carsForSale = await context.Car
                .Where(c => c.ForSell && !c.Sold)
                .ToListAsync();

            // ASSERT
            Assert.Equal(2, carsForSale.Count);
            Assert.All(carsForSale, c => Assert.True(c.ForSell));
            Assert.All(carsForSale, c => Assert.False(c.Sold));
        }



        [Fact]
        public async Task DeleteCar_ShouldNotAffectOtherCars()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var car1 = new Car { VinCode = "CAR-TO-KEEP", BrandId = 1, ModelId = 1, ConstructionYear = 2020, Mileage = 10000, SellPrice = 500 };
            var car2 = new Car { VinCode = "CAR-TO-DELETE", BrandId = 1, ModelId = 2, ConstructionYear = 2021, Mileage = 5000, SellPrice = 300 };

            context.Car.AddRange(car1, car2);
            await context.SaveChangesAsync();

            var car1Id = car1.Id;
            var car2Id = car2.Id;

            // ACT
            var carToDelete = await context.Car.FindAsync(car2Id);
            Assert.NotNull(carToDelete);

            context.Car.Remove(carToDelete);
            await context.SaveChangesAsync();

            // ASSERT
            var deletedCar = await context.Car.FindAsync(car2Id);
            var keptCar = await context.Car.FindAsync(car1Id);

            Assert.Null(deletedCar);
            Assert.NotNull(keptCar);
            Assert.Equal("CAR-TO-KEEP", keptCar.VinCode);
        }

 

        [Fact]
        public async Task AddCarWithCompleteData_ShouldSaveAllFields()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var car = new Car
            {
                VinCode = "COMPLETE-CAR-DATA",
                BrandId = 1,
                ModelId = 1,
                TrimLevel = "Premium",
                ConstructionYear = 2023,
                Mileage = 1000,
                ForSell = true,
                Sold = false,
                SellPrice = 250.50,
                ImagePath = "/img/user/test-image.jpg",
                RepairDescription = "Entretien complet effectué"
            };

            // ACT
            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ASSERT
            var savedCar = await context.Car
                .FirstOrDefaultAsync(c => c.VinCode == "COMPLETE-CAR-DATA");

            Assert.NotNull(savedCar);
            Assert.Equal("Premium", savedCar.TrimLevel);
            Assert.Equal(2023, savedCar.ConstructionYear);
            Assert.Equal(1000, savedCar.Mileage);
            Assert.Equal(250.50, savedCar.SellPrice);
            Assert.Equal("/img/user/test-image.jpg", savedCar.ImagePath);
            Assert.Equal("Entretien complet effectué", savedCar.RepairDescription);
            Assert.True(savedCar.ForSell);
            Assert.False(savedCar.Sold);
        }

    }
}
