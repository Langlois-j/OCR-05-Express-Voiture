using OCR_05_Express_Voiture.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OCR_05_Express_Voiture_Test.Unitaire
{
    /// <summary>
    /// Tests unitaires pour la validation du modèle Car
    /// </summary>
    public class CarValidationTests
    {
        [Fact]
        public void Car_WithValidData_ShouldPassValidation()
        {
            // ARRANGE
            var car = new Car
            {
                VinCode = "1HGBH41JXMN109186",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 50000,
                SellPrice = 1500.00
            };

            // ACT
            var validationResults = ValidateModel(car);

            // ASSERT
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Car_WithoutVinCode_ShouldFailValidation()
        {
            // ARRANGE
            var car = new Car
            {
                VinCode = "", 
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 50000,
                SellPrice = 1500.00
            };

            // ACT
            var validationResults = ValidateModel(car);

            // ASSERT
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("VinCode"));
        }

        [Theory]
        [InlineData(1989)] 
        [InlineData(2050)] 
        public void Car_WithInvalidYear_ShouldFailValidation(int year)
        {
            // ARRANGE
            var car = new Car
            {
                VinCode = "1HGBH41JXMN109186",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = year,
                Mileage = 50000,
                SellPrice = 1500.00
            };

            // ACT
            var validationResults = ValidateModel(car);

            // ASSERT
            Assert.NotEmpty(validationResults);
           
            Assert.Contains(validationResults, v =>
                v.ErrorMessage != null &&
                (v.ErrorMessage.Contains("année") || v.ErrorMessage.Contains("1990") || v.ErrorMessage.Contains(DateTime.Now.Year.ToString())));
        }

        [Theory]
        [InlineData(1990)] 
        [InlineData(2024)] 
        public void Car_WithValidYear_ShouldPassValidation(int year)
        {
            // ARRANGE
            var car = new Car
            {
                VinCode = "1HGBH41JXMN109186",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = year,
                Mileage = 50000,
                SellPrice = 1500.00
            };

            // ACT
            var validationResults = ValidateModel(car);

            // ASSERT
            Assert.Empty(validationResults);
        }

        [Fact]
        public void Car_DefaultValues_ShouldBeCorrect()
        {
            // ARRANGE & ACT
            var car = new Car
            {
                VinCode = "TEST",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 0,
                SellPrice = 0
            };

            // ASSERT
            Assert.True(car.ForSell); 
            Assert.False(car.Sold);   
        }

        [Fact]
        public void Car_OptionalFields_CanBeNull()
        {
            // ARRANGE
            var car = new Car
            {
                VinCode = "1HGBH41JXMN109186",
                BrandId = 1,
                ModelId = 1,
                ConstructionYear = 2020,
                Mileage = 50000,
                SellPrice = 1500.00,
                TrimLevel = null,
                ImagePath = null,
                RepairDescription = null
            };

            // ACT
            var validationResults = ValidateModel(car);

            // ASSERT
            Assert.Empty(validationResults);
        }

        /// <summary>
        /// Méthode helper pour valider un modèle
        /// </summary>
        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
