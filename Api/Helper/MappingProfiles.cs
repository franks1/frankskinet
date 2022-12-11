using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using OrderAddress=Core.Entities.OrderAggregate;
namespace Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>().
            ForMember(a => a.ProductType, x => x.MapFrom(b => b.ProductType.Name)).
            ForMember(a => a.ProductBrand, x => x.MapFrom(b => b.ProductBrand.Name)).
            ForMember(a => a.PrictureUrl, x => x.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress.Address>().ReverseMap();
        }

    }
}