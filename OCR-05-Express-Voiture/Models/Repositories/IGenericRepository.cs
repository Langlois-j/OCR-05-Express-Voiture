using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
   
        Task<T[]> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByNameAsync(string name);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task? DeleteAsync(Guid id);

     
            
    }
}
