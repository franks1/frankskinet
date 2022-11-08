using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        
        private readonly IProductRepository _repository;
   
        public ProductController(IProductRepository repository)
        {
            this._repository = repository;                  
        }        

        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var products=await this._repository.GetProductsAsync();
            return Ok(products);
        }

         [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var products=await this._repository.GetProductBrandsAsync();
            return Ok(products);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var result=await this._repository.GetProductTypesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product=await this._repository.GetProductByIdAsync(id);
            return Ok(product);
        }    
    }
}