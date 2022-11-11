using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.ProductSpecification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    // [ApiController]
    // [Route("api/products")]
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository repository;

        //   private readonly IProductRepository _repository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> productBrandRepository;
        private readonly IGenericRepository<ProductType> productTypeRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository repository,
        IGenericRepository<Product> productRepository,
        IGenericRepository<ProductBrand> productBrandRepository,
        IGenericRepository<ProductType> productTypeRepository,
        IMapper mapper
        )
        {
            this.repository = repository;
            //this._repository = repository;
            this.productRepository = productRepository;
            this.productBrandRepository = productBrandRepository;
            this.productTypeRepository = productTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> GetProducts()
        {
            var specification = new ProductWithTypeAndBrandSpecification();
            var products = await this.productRepository.ListAysnc(specification);
            var dto=mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            
            return Ok(dto);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands = await this.productBrandRepository.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var result = await this.productTypeRepository.ListAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var product = await this.productRepository.GetEntityWithSpec(specification);
            var dto = mapper.Map<ProductToReturnDto>(product);
            return Ok(dto);
        }
    }
}