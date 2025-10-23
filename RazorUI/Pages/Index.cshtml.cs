using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IGenericRepository<Product> _repo;

        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository, IGenericRepository<Product> repo)
        {
            _logger = logger;
            _productRepository = productRepository;
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
