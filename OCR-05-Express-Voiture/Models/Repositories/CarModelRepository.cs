using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarModelRepository : GenericRepository<CarModel>, ICarModelRepository
    {
        protected readonly DbSet<CarModel> _dbSet;

        public CarModelRepository(DbContext context) : base(context)
        {
            _dbSet = _context.Set<CarModel>();
        }

        public override async Task<CarModel?> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<CarModel[]> GetAllByBrandAsync(Guid carBrandId)
        {
            return await _dbSet
                .Where(cm => cm.BrandId == carBrandId)
                .OrderBy(cm => cm.Name)
                .ToArrayAsync();
        }
    }
}