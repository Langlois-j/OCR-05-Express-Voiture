using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Repositories;
using OCR_05_Express_Voiture.Models.Entities;
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


        // Méthodes CRUD asynchrones 
        [Fact]
        public async Task GetAllArrayAsync()
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



        //Synchronous CRUD operations
        [Fact]
        public void GetAll()
        {
            // Arrange
            var context = CreateInMemoryContext();
            // Seed data for testing
            var repository = new CarBrandRepository(context);

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
        [Fact]
        public void GetById() { Assert.Equal(false, true); }
        [Fact]
        public void GetByName() { Assert.Equal(false, true); }
        [Fact]
        public void Add() { Assert.Equal(false, true); }
        [Fact]
        public void Update() { Assert.Equal(false, true); }
        [Fact]
        public void Delete() { Assert.Equal(false, true); }

    }
  
}