using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface ICarModelRepository : IGenericRepository<CarModel>
    {
        Task<CarModel?> GetByNameAsync(string name);
        Task<CarModel[]> GetAllByBrandAsync(Guid carBrandId);
    }
}
