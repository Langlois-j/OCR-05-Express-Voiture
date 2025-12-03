using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using OCR_05_Express_Voiture.Models.Repositories;
using Xunit;
namespace OCR_05_Express_Voiture_Test
{
    public class CarModelRepositoryTest 
    {
        private Boolean _TestPasTout = false;
        private static DbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }


      
        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            var context = CreateInMemoryContext();
            // Seed data for testing
            var repository = new CarModelRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

         [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            var context = CreateInMemoryContext();
            // Seed data for testing
            var repository = new CarModelRepository(context);

            // Act
            var result = await repository.GetByIdAsync(SeedData.Models.Civic);

            // Assert
            Assert.NotNull(result);
            //Assert.NotEmpty(result);
            Assert.Equal("Civic", result.Name);
        }
        [Fact]
        public async Task GetByNameAsync() {
            // Arrange
            var context = CreateInMemoryContext();
            // Seed data for testing
            var repository = new CarModelRepository(context);

            // Act
            var result = await repository.GetByNameAsync("civic");

            // Assert
            Assert.NotNull(result);
            //Assert.NotEmpty(result);
            Assert.Equal(SeedData.Models.Civic, result.Id);
        }
       
        [Fact]
        public async Task AddAsync()
        {             // Arrange
            var context = CreateInMemoryContext();
 
            // Seed data for testing
            var repository = new CarModelRepository(context);
            var Input = new CarModel
            {
                Id = Guid.NewGuid(),
                Name = "TestModel",
                BrandId = SeedData.Brands.Honda
            };
            // Act
          //  var result = await repository.AddAsync(Input);

            // Assert
         //   Assert.NotNull(result);
            //Assert.NotEmpty(result);
           // Assert.Equal(SeedData.Models.Civic, result.Id); 
        }

        [Fact]
        public async Task UpdateAsync() { Assert.Equal(false, true); }
        [Fact]
        public async Task DeleteAsync() { Assert.Equal(false, true); }
        [Fact]
        public async Task GetAllByBrandAsync() {
            // Arrange
            var context = CreateInMemoryContext();
            // Seed data for testing
            var repository = new CarModelRepository(context);

            // Act
            var result = await repository.GetAllByBrandAsync(SeedData.Brands.Renault);

            // Assert         
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(SeedData.Brands.Renault, result[0].BrandId);
        }

    }
  
}