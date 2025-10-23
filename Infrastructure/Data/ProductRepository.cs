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
        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? srot)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(p => p.Brand == brand);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(p => p.Type == type);
            }

            if (!string.IsNullOrEmpty(srot))
            {
                switch (srot.ToLower())
                {
                    case "priceasc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "nameasc":
                        query = query.OrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    default:
                        break;
                }
            }
    
            //apply paging here
            //page_size * (page_number - 1), page_size
            return await query.ToListAsync();
            // return await _context.Products.ToListAsync();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
        public async Task<bool> SaveChagnesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetTypessAsync()
        {
            return await _context.Products.Select(p => p.Type).Distinct().ToListAsync();
        }
    }
}