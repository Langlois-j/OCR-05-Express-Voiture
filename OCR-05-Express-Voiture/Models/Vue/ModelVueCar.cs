namespace OCR_05_Express_Voiture.Models.Vue
{
    public class ModelVueCar
    {
        public int IdCar { get; set; }
        public int IdModel { get; set; }
        public int IdBrand { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string TrimLevel { get; set; }
        public string VimCode { get; set; }
        public int ConstructionYear { get; set; }
        public int Mileage { get; set; }
        public bool ForSell { get; set; }
        public bool Sold { get; set; }
    
        public Double? RepairAmont { get; set; }

        public List<String> Repair{ get; set; }

    }
}
