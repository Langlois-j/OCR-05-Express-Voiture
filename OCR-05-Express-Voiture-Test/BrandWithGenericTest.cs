using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Repositories;
using OCR_05_Express_Voiture.Models.Entities;
using Xunit;

namespace OCR_05_Express_Voiture_Test
{
    public class BrandWithGenericTest :GenericRepositoryTest<CarBrand, ICarBrandRepository>
    {
        protected override ICarBrandRepository CreateRepositoryTest(ApplicationDbContext context)
        {
            return new CarBrandRepository(context);
        }
        protected override ICarBrandRepository InsertEntityTest(ApplicationDbContext context)
        {
            var repository = CreateRepositoryTest(context);
            var brand = new CarBrand("TestBrand");
            context.CarBrands.Add(brand);
            return repository;
        }
    }
}
