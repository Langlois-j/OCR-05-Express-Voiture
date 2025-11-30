using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Models.Entities;
using System;
using System.Collections.Generic;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Méthodes CRUD asynchrones 
        public virtual Task<T[]> GetAllAsync()
        {
            return _dbSet.ToArrayAsync();
        }
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        // Méthode fournie par défaut ; les dépôts spécifiques peuvent override si nécessaire.
        public virtual Task<T?> GetByNameAsync(string name)
        {
            throw new NotImplementedException("GetByNameAsync must be overridden in concrete repository when needed.");
        }
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task? DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        //Synchronous CRUD operations
        public virtual T[] GetAll()
        {
            return _dbSet.ToArray();
        }
        public virtual T? GetById(Guid Id)
        {
            return _dbSet.Find(Id);
        }
        // Méthode fournie par défaut ; les dépôts spécifiques peuvent override si nécessaire.
        public virtual T? GetByName(string name)
        {
            throw new NotImplementedException("GetByNameAsync must be overridden in concrete repository when needed.");
        }
        public virtual T Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public virtual Boolean? Delete(Guid id)
        {
            var entity =  GetById(id);
            if (entity is not null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
               return true;
            }
            return false;
        }
    }
}
