using System.Collections.Generic;
namespace OCR_05_Express_Voiture.Models.Entities
{
    public partial class CarBrand
    {
        public CarBrand()
        {
        }

        public CarBrand(string name) 
       { 
           Id= Guid.NewGuid();
           Name = name;
       }
        public Guid Id {get ;set;}
        public string requi Name { get; set; }

    }
}
