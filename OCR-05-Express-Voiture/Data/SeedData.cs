namespace OCR_05_Express_Voiture.Data
{
    public class SeedData
    {
        public static class Brands
        {
            public static readonly Guid Renault     = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000001");
            public static readonly Guid Mazda       = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000002");
            public static readonly Guid Jeep        = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000003");
            public static readonly Guid Ford        = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000004");
            public static readonly Guid Honda       = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000005");
            public static readonly Guid Volkswagen  = Guid.Parse("21c9b0b6-1a2d-4f61-8fcb-100000000006");
        }

        public static class Models
        {
            public static readonly Guid Miata    = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000001");
            public static readonly Guid Cx5      = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000002");
            public static readonly Guid Wrangler = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000003");
            public static readonly Guid Cherokee = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000004");
            public static readonly Guid Mustang  = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000005");
            public static readonly Guid F150     = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000006");
            public static readonly Guid Civic    = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000007");
            public static readonly Guid Accord   = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000008");
            public static readonly Guid Clio     = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000009");
            public static readonly Guid Megane   = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000010");
            public static readonly Guid Golf     = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-200000000011");
            public static readonly Guid Passat   = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-000000000062");
        }

        public static class RepairType
        {
            public static readonly Guid RestaurationComplete = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000001");
            public static readonly Guid RotuleAvant          = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000002");
            public static readonly Guid RotuleArriere        = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");
            public static readonly Guid Radiateur            = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");
            public static readonly Guid PneusAvant           = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");
            public static readonly Guid PneusArriere         = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");
            public static readonly Guid Freins               = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");
            public static readonly Guid Climatisation        = Guid.Parse("31c9b0b6-1a2d-4f61-8fcb-300000000003");

        }

    }
}
