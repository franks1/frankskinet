using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
   
        public ProductController(StoreContext context)
        {
            this._context = context;            
        }        

        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var products=await this._context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product=await this._context.Product.FindAsync(id);
            return Ok(product);
        }



    }
}