namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BrandId { get; set; }
        public virtual CarBrand? CarBrand { get; set; }  

    }
}
