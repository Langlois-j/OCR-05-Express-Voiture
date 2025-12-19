using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using System.Threading.Tasks;
using static OCR_05_Express_Voiture.Data.SeedData;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarRepairRepository : GenericRepository<CarRepair>, ICarRepairRepository
    {
        private static List<CarRepair> _Repair;
        protected readonly DbSet<CarRepair> _dbSet;

        public CarRepairRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = _context.Set<CarRepair>();
        }



    }
}
