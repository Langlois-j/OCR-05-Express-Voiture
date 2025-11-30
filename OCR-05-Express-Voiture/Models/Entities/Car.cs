namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public required string VinCode { get; set; }

        public Guid ModelId { get; set; }
        public virtual CarModel Model { get; set; } = null!;

        public string? TrimLevel { get; set; }

        public int ConstructionYear { get; set; }

        public Guid? PurchaseId { get; set; }
        public virtual PurchaseSale? Purchase { get; set; }

        public Guid? SaleId { get; set; }
        public virtual PurchaseSale? Sale { get; set; }

        public double RepairAmount { get; set; }



    }
}
