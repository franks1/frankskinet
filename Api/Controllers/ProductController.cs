using Core.Entities;
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
        
     //   private readonly IProductRepository _repository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> productBrandRepository;
        private readonly IGenericRepository<ProductType> productTypeRepository;

        public ProductController(IProductRepository repository,
        IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository
        )
        {
//this._repository = repository;
            this.productRepository = productRepository;
            this.productBrandRepository = productBrandRepository;
            this.productTypeRepository = productTypeRepository;
        }        

        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var products=await this.productRepository.ListAllAsync();
            return Ok(products);
        }

         [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands=await this.productBrandRepository.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var result=await this.productTypeRepository.ListAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product=await this.productRepository.GetByIdAsync(id);
            return Ok(product);
        }    
    }
}