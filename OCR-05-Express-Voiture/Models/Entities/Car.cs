namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public string? VimCode { get; set; }

        public required CarModel Model { get; set; }

        public string? Appearance { get; set; }

        public int ConstructionYear { get; set; }

        public Guid PurchaseId { get; set; }

        public Guid SaleId { get; set; }

        public DateOnly? SellingDate { get; set; }

        public double RepearAmount { get; set; }

        public virtual ICollection<Repear> Repears { get; set; }

        public virtual PurchaseSale Purchase { get; set; }

        public virtual PurchaseSale Sale { get; set; }


    }
}
