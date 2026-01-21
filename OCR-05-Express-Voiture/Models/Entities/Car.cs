using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(
            ErrorMessage = "Code VIM nécéssaire.")]
        public required string VinCode { get; set; }
        [Required(
           ErrorMessage = "Constructeur nécéssaire.")]
        public int CarBrandId { get; set; }
        public virtual CarBrand? Brand { get; set; } = null!;
            [Required(
        ErrorMessage = "Model nécéssaire.")]

        public int CarModelId { get; set; }
        public virtual CarModel? Model { get; set; } = null!;

        public string? TrimLevel { get; set; }
        [Required(
ErrorMessage = "Année de constrcution nécéssaire.")]
        public int ConstructionYear { get; set; }
        [Required(
ErrorMessage = "Kilomettrage nécéssaire.")]
        public int Mileage { get; set; }


        public Boolean ForSell { get; set; }
        public Boolean Sold { get; set; }

        [Required(ErrorMessage = "Montant réparation nécéssaire.")]
        public double RepairAmount { get; set; }
        public string? ImagePath { get; set; }
        public string? RepairDescription { get; set; }
    }
}