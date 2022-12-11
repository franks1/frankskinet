using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace Api.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration config;

        public ProductUrlResolver(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination,
         string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
             return config["ApiUrl"]+source.PictureUrl;
            }
            return string.Empty;
        }
    }
    
    public class ProductOrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration config;

        public ProductOrderUrlResolver(IConfiguration config)
        {
            this.config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination,
            string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Ordered.PictureUrl))
            {
                return config["ApiUrl"]+source.Ordered.PictureUrl;
            }
            return string.Empty;
        }
    }
    
}