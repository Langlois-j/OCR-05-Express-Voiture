namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public required string VinCode { get; set; }

        public int BrandId { get; set; }
        public virtual CarBrand Brand { get; set; } = null!;

        public int ModelId { get; set; }
        public virtual CarModel Model { get; set; } = null!;

        public string? TrimLevel { get; set; }

        public int ConstructionYear { get; set; }

        public int Mileage { get; set; }

        public Boolean ForSell { get; set; }
        public Boolean Sold { get; set; }

        public double RepairAmount { get; set; }



    }
}
