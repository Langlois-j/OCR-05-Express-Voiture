namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarBrand
    {
        public CarBrand(string name) { 
            Id= Guid.NewGuid();
            Name = name;
        }
        public Guid Id {get ;set;}
        public string Name { get; set; }

    }
}
