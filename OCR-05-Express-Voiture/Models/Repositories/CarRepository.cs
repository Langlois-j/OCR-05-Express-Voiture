using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private static List<Car> _car;
        protected readonly DbSet<Car> _dbSet;

        public CarRepository(ApplicationDbContext context) : base(context)
        {
            _dbSet = _context.Set<Car>();
        }



    }
}
