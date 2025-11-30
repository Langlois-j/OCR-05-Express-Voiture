namespace OCR_05_Express_Voiture.Models.Entities
{
    public class Repair
    {
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public Guid RepairTypeId { get; set; }

     public virtual Car Car { get; set; }
     public virtual RepairType RepairType { get; set; }
    }
}
