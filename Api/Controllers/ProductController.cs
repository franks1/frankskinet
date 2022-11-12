using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Dto;
using Api.Errors;
using Api.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.ProductSpecification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    // [ApiController]
    // [Route("api/products")]
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository repository;

        //   private readonly IProductRepository _repository;
        private readonly IGenericRepository<Product> productRepository;
        private readonly IGenericRepository<ProductBrand> productBrandRepository;
        private readonly IGenericRepository<ProductType> productTypeRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository repository,
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
        public async Task<IActionResult> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var specification = new ProductWithTypeAndBrandSpecification(productParams);
            var countspecification = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await productRepository.CountAsync(countspecification);
            var products = await this.productRepository.ListAysnc(specification);
            var dto = mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            var paginationresponse = new Pagination<ProductToReturnDto>(productParams.PageSize,
            productParams.PageIndex, totalItems, dto);
            return Ok(paginationresponse);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecification(id);
            var product = await this.productRepository.GetEntityWithSpec(specification);
            if (product is null) return NotFound(new ApiResponse(404));


            var dto = mapper.Map<ProductToReturnDto>(product);
            return Ok(dto);
        }
    }
}