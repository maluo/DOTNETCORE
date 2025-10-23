using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Any(e => e.Id == id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            //call the save changes async afterwards
        }
        
        //sepc pattern
        public async Task<IReadOnlyList<T>> ListAsync(ISpec<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }
        public Task<T?> GetEntityWithSpecAsync(ISpec<T> spec)
        {
            return ApplySpec(spec).FirstOrDefaultAsync();
        }

        //passing query and spec to the evaluator
        private IQueryable<T> ApplySpec(ISpec<T> spec)
        {
            return SpecEval<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        private IQueryable<TResult> ApplySpec<TResult>(ISpec<T, TResult> spec)
        {
            return SpecEval<T>.GetQuery<T,TResult>(_context.Set<T>().AsQueryable(), spec);
        }

        public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpec<T, TResult> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpec<T, TResult> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }
    }
}