namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Repear
    {
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public Guid RepearTypeId { get; set; }

     public virtual Car Car { get; set; }
     public virtual RepearType RepearType { get; set; }
    }
}
