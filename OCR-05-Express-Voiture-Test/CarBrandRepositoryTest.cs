using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Repositories;
using Xunit;
namespace OCR_05_Express_Voiture_Test
{
    public class CarBrandRepositoryTest
    {
        private static DbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDb")
                            .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task GetAllArrayAsync()
        {
            // Arrange
            var context = CreateInMemoryContext();
            var repository = new CarBrandRepository(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        //    [Fact]
        // public void GetById()
        //{
        // Arrange
        // var repository = new CarBrandRepository();
        //  var controle = repository.GetAllArray();
        //
        // // Act
        // var result = repository.GetById(controle[1].Id);
        //
        // // Assert
        // Assert.Equal (result, controle[1]);
        //
    }
    //       [Fact]
    // public void GetByName()
    // {
    //// Arrange
    //var repository = new CarBrandRepository();
    //var controle = repository.GetAllArray();
    ////String CarMkerName = controle[1].Name;
    //// Act
    //
    //var resultLower = repository.GetByName(controle[1].Name.ToLower());
    //var resultUpper = repository.GetByName(controle[1].Name.ToUpper());
    //
    //// Assert
    //     Assert.Equal(resultLower, controle[1]);
    //     Assert.Equal(resultUpper, controle[1]);

    //  }
    //}
}