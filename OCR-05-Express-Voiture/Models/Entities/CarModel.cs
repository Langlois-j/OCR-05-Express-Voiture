namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public CarBrand CarBrand { get; set; }  

    }
}
