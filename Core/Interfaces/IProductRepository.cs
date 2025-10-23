using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? srot);
        //could be null if not found
        Task<Product?> GetProductByIdAsync(int id);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<bool> SaveChagnesAsync();
        bool ProductExists(int id);
        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypessAsync();
    }
}