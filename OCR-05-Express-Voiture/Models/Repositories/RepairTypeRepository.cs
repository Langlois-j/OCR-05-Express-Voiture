using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using System.Threading.Tasks;
using static OCR_05_Express_Voiture.Data.SeedData;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class RepairTypeRepository : GenericRepository<RepairType>, IRepairTypeRepository
    {
        private static List<RepairType> _RepairType;
        protected readonly DbSet<RepairType> _dbSet;
        public RepairTypeRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = _context.Set<RepairType>();
        }


    }
}
