using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductModel> _logger;
        private readonly IGenericRepository<Product> _repo;

        public ProductModel(IProductRepository productRepository, ILogger<ProductModel> logger, IGenericRepository<Product> repo) { 
            _productRepository = productRepository;
            _logger = logger;
            ProductList = new List<Product>();
            _repo = repo;
        }

        public IReadOnlyList<Product> ProductList { get; set; }

        public async Task OnGetAsync()
        {
            ProductList = await _repo.ListAllAsync();
            _logger.LogInformation("OnGetAsync executed.");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Simulate some asynchronous operation
            await Task.Delay(1000);
            _logger.LogInformation("Post request processed.");
            return RedirectToPage();
        }
    }
}
