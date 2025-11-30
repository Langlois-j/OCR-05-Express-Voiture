namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public virtual CarBrand? CarBrand { get; set; }  

    }
}
