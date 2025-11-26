
using OCR_05_Express_Voiture.Models.Repositories;
using Xunit;
namespace OCR_05_Express_Voiture_Test
{
    public class CarMakerTest
    {
        [Fact]
        public void GetAllArray()
        {
            // Arrange
            var repository = new CarMakerRepository();

            // Act
            var result = repository.GetAllArray();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetById()
        {
            // Arrange
            var repository = new CarMakerRepository();
            var controle = repository.GetAllArray();

            // Act
            var result = repository.GetById(controle[1].Id);

            // Assert
            Assert.Equal (result, controle[1]);
          
        }
        [Fact]
        public void GetByName()
        {
            // Arrange
            var repository = new CarMakerRepository();
            var controle = repository.GetAllArray();
            //String CarMkerName = controle[1].Name;
            // Act
            
            var resultLower = repository.GetByName(controle[1].Name.ToLower());
            var resultUpper = repository.GetByName(controle[1].Name.ToUpper());

            // Assert
            Assert.Equal(resultLower, controle[1]);
            Assert.Equal(resultUpper, controle[1]);

        }
    }
}