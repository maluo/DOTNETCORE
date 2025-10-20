using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
        public void DeleteProduct(Product p)
        {
            var product = _context.Products.Find(p.Id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
        public async Task<bool> SaveChagnesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}