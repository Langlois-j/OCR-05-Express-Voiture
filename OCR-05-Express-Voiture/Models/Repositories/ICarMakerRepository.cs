using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface ICarMakerRepository
    {

        public CarMaker[] GetAllArray();
        public CarMaker GetById(Guid Id);
        public CarMaker GetByName(String name);
    }
}
