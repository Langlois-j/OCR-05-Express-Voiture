using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Repositories;
using OCR_05_Express_Voiture.Models.Entities;
using Xunit;
namespace OCR_05_Express_Voiture_Test
{
    public class CarBrandRepositoryTest
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
            var repository = new CarBrandRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            Assert.Equal(false, true);
        }
        [Fact]
        public async Task GetByNameAsync() { Assert.Equal(false, true); }

        [Fact]
        public async Task AddAsync() { Assert.Equal(false, true); }

        [Fact]
        public async Task UpdateAsync() { Assert.Equal(false, true); }
        [Fact]
        public async Task DeleteAsync() { Assert.Equal(false, true); }

    }

  
}