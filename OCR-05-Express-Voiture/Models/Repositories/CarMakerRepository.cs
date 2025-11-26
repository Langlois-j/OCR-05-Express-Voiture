using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarMakerRepository : ICarMakerRepository
    {
        private static List<CarMaker> _carmaker;
        public CarMakerRepository()
        {
            if (_carmaker == null)
            {
                _carmaker = new List<CarMaker>();
                GenerateCarMakerData();
            }

        }

        private void GenerateCarMakerData()
        {
            _carmaker.Add(new CarMaker("Renault"));
            _carmaker.Add(new CarMaker("Mazda"));
            _carmaker.Add(new CarMaker("Jeep"));
            _carmaker.Add(new CarMaker("Ford"));
            _carmaker.Add(new CarMaker("Honda"));
            _carmaker.Add(new CarMaker("Volkswagen"));
        }
        public CarMaker[] GetAllArray()
        {
            List<CarMaker> list = _carmaker.ToList();
            return list.ToArray();
        }
        public CarMaker GetById(Guid id)
        {
            return GetAllArray().FirstOrDefault(Obj => Obj.Id == id);
        }
        public CarMaker GetByName(String name)
        {
            return GetAllArray().FirstOrDefault(Obj => Obj.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
