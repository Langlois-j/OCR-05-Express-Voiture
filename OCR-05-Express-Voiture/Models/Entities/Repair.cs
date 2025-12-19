using System.ComponentModel.DataAnnotations;

namespace OCR_05_Express_Voiture.Models.Entities
{
    public class CarRepair
    {
        [Key]
        public int Id { get; set; }
    public int CarId { get; set; }
    public int RepairTypeId { get; set; }

     public virtual Car Car { get; set; }
     public virtual RepairType RepairType { get; set; }
    }
}
