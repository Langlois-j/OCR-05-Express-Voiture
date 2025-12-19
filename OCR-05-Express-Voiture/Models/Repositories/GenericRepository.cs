using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using OCR_05_Express_Voiture.Data;
using OCR_05_Express_Voiture.Models.Entities;
using System;
using System.Collections.Generic;

namespace OCR_05_Express_Voiture.Models.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Méthodes CRUD asynchrones 
        public virtual Task<T[]> GetAllAsync()
        {
            return _dbSet.ToArrayAsync();
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
   
        public virtual async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
        }
        public virtual async Task<bool> DeleteAsync(int id)
        {
            
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                bool SaveStatut;
                _dbSet.Remove(entity);
                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    return false;
                }

            }
            return true;
        }
    }
}
