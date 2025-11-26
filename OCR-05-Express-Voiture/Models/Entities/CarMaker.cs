namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarMaker
    {
        public CarMaker(string name) { 
            Id= Guid.NewGuid();
            Name = name;
        }
        public Guid Id {get ;set;}
        public string Name { get; set; }

    }
}
