using System.ComponentModel.DataAnnotations;

namespace OCR_05_Express_Voiture.Models.Entities
{
    public partial class CarBrand
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

    }
}
