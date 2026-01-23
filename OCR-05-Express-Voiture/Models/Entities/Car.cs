using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Code VIM nécessaire.")]
        public string VinCode { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Constructeur nécessaire.")]
        public int BrandId { get; set; }
        public virtual CarBrand? Brand { get; set; }
        
        [Required(ErrorMessage = "Modèle nécessaire.")]
        public int ModelId { get; set; }
        public virtual CarModel? Model { get; set; }

        public string? TrimLevel { get; set; }
        
        [Required(ErrorMessage = "Année de construction nécessaire.")]
        public int ConstructionYear { get; set; }

        [Required(ErrorMessage = "Kilométrage nécessaire.")]
        public int Mileage { get; set; } 

        public bool ForSell { get; set; } = true;
        public bool Sold { get; set; }=false;

        [Required(ErrorMessage = "Montant réparation nécessaire.")]
        public double RepairAmount { get; set; }
        
        public string? ImagePath { get; set; }
        public string? RepairDescription { get; set; }
    }
}