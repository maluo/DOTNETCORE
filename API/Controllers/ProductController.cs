using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IProductRepository _productRepo;

        public ProductController(StoreContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepo = productRepository;
        }

        [HttpGet]
        //query could perform complex database operations
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            return Ok(await _productRepo.GetProductsAsync());
        }

        [HttpGet("{id:int}")] //api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
            // var product = await _productRepo.GetProductByIdAsync(id);
            // return (product == null) ? NotFound() : product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {

            // Using the repository to add a new product
            _productRepo.CreateProduct(product);
            if (await _productRepo.SaveChagnesAsync())
            {
                //It will directs the site to the new location
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Could not create the product");

            // Alternatively, using the context directly:
            // _context.Products.Add(product);
            // await _context.SaveChangesAsync();
            //return product;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Can not update this product");
            }
            _productRepo.UpdateProduct(product);
            if (await _productRepo.SaveChagnesAsync())
            {
                return NoContent();
            }
            return BadRequest("Could not update the product");

            // _context.Entry(product).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            // return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            _productRepo.DeleteProduct(product);
            if (await _productRepo.SaveChagnesAsync())
            {
                return NoContent();
            }
            return BadRequest("Could not delete the product");

            //var product = await _context.Products.FindAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }
            // _context.Products.Remove(product);
            // await _context.SaveChangesAsync();
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
        {
            return Ok(await _productRepo.GetBrandsAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            return Ok(await _productRepo.GetTypessAsync());
        }
    }
}