using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface ICarModelRepository
    {

        public CarModel [] GetAllArray();
        public CarModel GetById(Guid Id);
        public CarModel GetByName(String name);
    }
}
