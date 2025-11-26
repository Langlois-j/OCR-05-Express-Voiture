using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarBrandRepository : GenericRepository<CarBrand>, ICarBrandRepository
    {
        private static List<CarBrand> _carbrand;
        protected readonly DbSet<CarBrand> _dbSet;

        public CarBrandRepository(DbContext context) : base(context)
        {
            _dbSet = _context.Set<CarBrand>();
        }

        public override async Task<CarBrand?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

    }
}
