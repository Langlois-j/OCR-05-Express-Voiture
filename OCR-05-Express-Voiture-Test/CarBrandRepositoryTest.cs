using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Repositories;
using OCR_05_Express_Voiture.Models.Entities;
using Xunit;
namespace OCR_05_Express_Voiture_Test
{
    public class CarBrandRepositoryTest : GenericRepositoryTest<CarBrand, ICarBrandRepository>
    {
        protected override ICarBrandRepository CreateRepositoryTest(ApplicationDbContext context)
        {
            return new CarBrandRepository(context);
        }
        protected override ICarBrandRepository InsertEntityTest(ApplicationDbContext context)
        {
            var repository = CreateRepositoryTest(context);
            var brand = new CarBrand("TestBrand");
            //context.Add(brand);
            context.CarBrand.Add(brand);
            context.SaveChangesAsync().Wait();
            return repository;
        }
        protected override ICarBrandRepository CreateValideEntityTest()
        {
            return new CarBrand()
            {
                Id = Guid.NewGuid(),
                Name = "Test Brand"
            };
        }
        protected override Guid GetEntityTestId(TEntity entity);
    }

  
