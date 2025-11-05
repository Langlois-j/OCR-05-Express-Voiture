namespace OCR_05_Express_Voiture.Models.Entities
{
    public class PurchaseSale
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public Double Price { get; set; }

        public Guid CarId { get; set; }

        public virtual required Car Car { get; set; }
    }
}
