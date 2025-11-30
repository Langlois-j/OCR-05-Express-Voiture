using OCR_05_Express_Voiture.Models.Entities;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
        //Assynchronous CRUD operations
        Task<T[]> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByNameAsync(string name);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task? DeleteAsync(Guid id);

        //Synchronous CRUD operations
        T[] GetAll();
        T? GetById(Guid id);
        T? GetByName(string name);
        T Add(T entity);
        T Update(T entity);
       Boolean? Delete(Guid id);
            
    }
}
