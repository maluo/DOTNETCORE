using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : Base
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
        bool Exists(int id);
        
        //spec pattern
        Task<T?> GetEntityWithSpecAsync(ISpec<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpec<T> spec);

        //need to review these methods
        Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpec<T, TResult> spec);
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpec<T, TResult> spec);
        Task<int> CountAsync(ISpec<T> spec);
    }   
}