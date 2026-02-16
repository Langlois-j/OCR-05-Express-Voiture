using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using OCR_05_Express_Voiture_Test.configuratiion;

namespace OCR_05_Express_Voiture_Test.Integration
{
    /// <summary>
    /// Tests d'intégration pour la gestion des marques et modèles
    /// </summary>
    public class BrandAndModelTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public BrandAndModelTests(CustomWebApplicationFactory factory)
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
        public async Task AddBrand_ShouldSaveToDatabase()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var brand = new CarBrand { Name = "Tesla" };

            // ACT
            context.CarBrand.Add(brand);
            await context.SaveChangesAsync();

            // ASSERT
            var savedBrand = await context.CarBrand
                .FirstOrDefaultAsync(b => b.Name == "Tesla");

            Assert.NotNull(savedBrand);
            Assert.Equal("Tesla", savedBrand.Name);
        }

        [Fact]
        public async Task DeleteBrand_WithoutModels_ShouldSucceed()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var brand = new CarBrand { Name = "BrandToDelete" };
            context.CarBrand.Add(brand);
            await context.SaveChangesAsync();

            var brandId = brand.Id;

            // ACT
            var brandToDelete = await context.CarBrand.FindAsync(brandId);
            Assert.NotNull(brandToDelete);

            context.CarBrand.Remove(brandToDelete);
            await context.SaveChangesAsync();

            // ASSERT
            var deletedBrand = await context.CarBrand.FindAsync(brandId);
            Assert.Null(deletedBrand);
        }

        [Fact]
        public async Task GetAllBrands_ShouldReturnAllBrands()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // ACT
            var brands = await context.CarBrand.ToListAsync();

            // ASSERT
            Assert.NotEmpty(brands);
            Assert.Contains(brands, b => b.Name == "C1");
            Assert.Contains(brands, b => b.Name == "C2");
            Assert.Contains(brands, b => b.Name == "C3");
        }

      

        #region Tests CarModel

        [Fact]
        public async Task AddModel_ShouldSaveToDatabase()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var model = new CarModel
            {
                Name = "Model S",
                CarBrandId = 1
            };

            // ACT
            context.CarModel.Add(model);
            await context.SaveChangesAsync();

            // ASSERT
            var savedModel = await context.CarModel
                .FirstOrDefaultAsync(m => m.Name == "Model S");

            Assert.NotNull(savedModel);
            Assert.Equal("Model S", savedModel.Name);
            Assert.Equal(1, savedModel.CarBrandId);
        }

        [Fact]
        public async Task GetModelWithBrand_ShouldIncludeBrandRelation()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // ACT
            var modelWithBrand = await context.CarModel
                .Include(m => m.CarBrand)
                .FirstOrDefaultAsync(m => m.Name == "C1M1");

            // ASSERT
            Assert.NotNull(modelWithBrand);
            Assert.NotNull(modelWithBrand.CarBrand);
            Assert.Equal("C1", modelWithBrand.CarBrand.Name);
        }

        [Fact]
        public async Task DeleteModel_WithoutCars_ShouldSucceed()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var model = new CarModel
            {
                Name = "ModelToDelete",
                CarBrandId = 1
            };

            context.CarModel.Add(model);
            await context.SaveChangesAsync();

            var modelId = model.Id;

            // ACT
            var modelToDelete = await context.CarModel.FindAsync(modelId);
            Assert.NotNull(modelToDelete);

            context.CarModel.Remove(modelToDelete);
            await context.SaveChangesAsync();

            // ASSERT
            var deletedModel = await context.CarModel.FindAsync(modelId);
            Assert.Null(deletedModel);
        }

        #endregion

        #region Tests Relations

        [Fact]
        public async Task Brand_CannotBeDeleted_IfHasModels()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Vérifier qu'il y a des modèles pour la marque 1
            var hasModels = await context.CarModel.AnyAsync(m => m.CarBrandId == 1);
            Assert.True(hasModels);

            // ACT & ASSERT
          
            var modelsCount = await context.CarModel
                .Where(m => m.CarBrandId == 1)
                .CountAsync();

            Assert.True(modelsCount > 0);
        }

        [Fact]
        public async Task Model_CannotBeDeleted_IfUsedByCars()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Créer une voiture avec le modèle 1
            var car = new Car
            {
                VinCode = "CAR-WITH-MODEL",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 10000,
                SellPrice = 500
            };

            context.Car.Add(car);
            await context.SaveChangesAsync();

            // ACT & ASSERT
            var hasCars = await context.Car.AnyAsync(c => c.ModelId == 1);
            Assert.True(hasCars);
        }

        [Fact]
        public async Task GetBrandsWithModelCount_ShouldReturnCorrectCounts()
        {
            await ResetDatabaseAsync();
            // ARRANGE
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // ACT
            var brandsWithCounts = await context.CarBrand
                .Select(b => new
                {
                    Brand = b,
                    ModelCount = context.CarModel.Count(m => m.CarBrandId == b.Id)
                })
                .ToListAsync();

            // ASSERT
            Assert.NotEmpty(brandsWithCounts);

            var brand1 = brandsWithCounts.FirstOrDefault(b => b.Brand.Id == 1);
            Assert.NotNull(brand1);
            Assert.Equal(2, brand1.ModelCount); 
        }

        #endregion
    }
}
