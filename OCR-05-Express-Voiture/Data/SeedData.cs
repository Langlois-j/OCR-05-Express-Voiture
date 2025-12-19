namespace OCR_05_Express_Voiture.Data
{
    public class SeedData
    {
        public static class SeedBrands
        {
            public static readonly int Renault = 1;
            public static readonly int Mazda = 2;
            public static readonly int Jeep = 3;
            public static readonly int Ford = 4;
            public static readonly int Honda = 5;
            public static readonly int Volkswagen = 6;
        }

        public static class SeedModels
        {
            public static readonly int Miata = 1;
            public static readonly int Cx5 = 2;
            public static readonly int Wrangler = 3;
            public static readonly int Cherokee = 4;
            public static readonly int Mustang = 5;
            public static readonly int F150 = 6;
            public static readonly int Civic = 7;
            public static readonly int Accord = 8;
            public static readonly int Clio = 9;
            public static readonly int Megane = 10;
            public static readonly int Golf = 11;
            public static readonly int Passat = 12;
        }

        public static class SeedRepairType
        {
            public static readonly int RestaurationComplete = 1;
            public static readonly int RotuleAvant = 2;
            public static readonly int RotuleArriere = 3;
            public static readonly int Radiateur = 4;
            public static readonly int PneusAvant = 5;
            public static readonly int PneusArriere = 6;
            public static readonly int Freins = 7;
            public static readonly int Climatisation = 8;
        }
    }
}
