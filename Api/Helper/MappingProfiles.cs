using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Core.Entities;

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
            //ReverseMap();
        }

    }
}