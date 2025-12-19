using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
   
        Task<T[]> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

     
            
    }
}
