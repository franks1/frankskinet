using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using OrderAddress = Core.Entities.OrderAggregate;

namespace Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(a => a.ProductType, x => x.MapFrom(b => b.ProductType.Name))
                .ForMember(a => a.ProductBrand, x => x.MapFrom(b => b.ProductBrand.Name))
                .ForMember(a => a.PrictureUrl, x => x.MapFrom<ProductUrlResolver>());

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress.Address>().ReverseMap();

            CreateMap<OrderAddress.Order, OrderToReturnDto>()
                .ForMember(a => a.OrderStatus,
                    x =>
                        x.MapFrom(y => (y.OrderStatus)))
                .ForMember(a => a.Delivery, b
                    => b.MapFrom(c => c.Delivery.ShortName))
                .ForMember(a => a.ShippingPrice, b
                    => b.MapFrom(c => c.Delivery.Price));

            CreateMap<OrderAddress.OrderItem, OrderItemDto>()
                .ForMember(a => a.ProductName,
                    b => b.MapFrom(c => c.Ordered.ProductName))
                .ForMember(a => a.ProductId,
                    b => b.MapFrom(c => c.Ordered.ProductItemId))
                .ForMember(a => a.PictureUrl, b
                    => b.MapFrom<ProductOrderUrlResolver>());


        }
    }
}