using Microsoft.IdentityModel.Tokens;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarModelRepository : ICarModelRepository
    {
        private static List<CarModel> _carmodel;
        public CarModelRepository() {

            if (_carmodel == null)
            {
                _carmodel = new List<CarMaker>();
                GenerateCarModelData();
            }
                    private void GenerateCarModelData()
        {
            private 
            carmodel.Add(new CarMaker("Renault"));
            carmodel.Add(new CarMaker("Mazda"));
            carmodel.Add(new CarMaker("Jeep"));
            carmodel.Add(new CarMaker("Ford"));
            carmodel.Add(new CarMaker("Honda"));
            carmodel.Add(new CarMaker("Volkswagen"));
        }
    }

    }
}
