using OCR_05_Express_Voiture.Data;
using System.Collections.Generic;
namespace OCR_05_Express_Voiture.Models.Entities
{
    public partial class CarBrand : ApplicationDbContext
    {
        public int Id {get ;set;}
        public required string  Name { get; set; }

    }
}
