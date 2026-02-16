using System.ComponentModel.DataAnnotations;

namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarModel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CarBrandId { get; set; }
        public virtual CarBrand? CarBrand { get; set; }

    }
}
