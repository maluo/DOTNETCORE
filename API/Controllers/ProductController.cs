using Core.Entities;
using Core.Interfaces;
using Core.Specs;
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
        private readonly IGenericRepository<Product> _genericRepo;

        public ProductController(StoreContext context, IProductRepository productRepository,
        IGenericRepository<Product> genericRepository)
        {
            _context = context;
            _productRepo = productRepository;
            _genericRepo = genericRepository;
        }

        /* Basic loading
        *  public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(){}
        *  Filtering and sorting
        *  public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort){}
        *  Using specification pattern
        *  public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams productParams){}
        */  
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            //return Ok(await _productRepo.GetProductsAsync(brand,type,sort));
            var spec = new ProductSpec(brand, type, sort);
            var products = await _genericRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id:int}")] //api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // var product = await _productRepo.GetProductByIdAsync(id);
            // return (product == null) ? NotFound() : product;

            // var product = await _productRepo.GetProductByIdAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }

            var product = await _genericRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {

            // Alternatively, using the context directly:
            // _context.Products.Add(product);
            // await _context.SaveChangesAsync();
            //return product;

            // Using the repository to add a new product
            // _productRepo.CreateProduct(product);
            // if (await _productRepo.SaveChagnesAsync())
            // {
            //     //It will directs the site to the new location
            //     return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            // }
            // return BadRequest("Could not create the product");

            _genericRepo.Add(product);
            if (await _genericRepo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            return BadRequest("Could not create the product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            // _context.Entry(product).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            // return product;

            if (id != product.Id || !ProductExists(id))
            {
                return BadRequest("Can not update this product");
            }

            // _productRepo.UpdateProduct(product);
            // if (await _productRepo.SaveChagnesAsync())
            // {
            //     return NoContent();
            // }
            // return BadRequest("Could not update the product");

            _genericRepo.Update(product);
            if (await _genericRepo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Could not update the product");
        }

        private bool ProductExists(int id)
        {
            //return _context.Products.Any(e => e.Id == id);
            return _genericRepo.Exists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }
            // _context.Products.Remove(product);
            // await _context.SaveChangesAsync();

            // var product = await _productRepo.GetProductByIdAsync(id);
            // if (product == null) return NotFound();
            // _productRepo.DeleteProduct(product);
            // if (await _productRepo.SaveChagnesAsync())
            // {
            //     return NoContent();
            // }
            // return BadRequest("Could not delete the product");

            var product = await _genericRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            _genericRepo.Delete(product);
            if (await _genericRepo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Could not delete the product");
        }

        
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
        {
            //return Ok(await _productRepo.GetBrandsAsync());
            var spec = new BrandListSpec();
            return Ok(await _genericRepo.ListAsync(spec));
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            //return Ok(await _productRepo.GetTypessAsync());
            var spec = new TypeListSpec();
            return Ok(await _genericRepo.ListAsync(spec));
        }
    }
}