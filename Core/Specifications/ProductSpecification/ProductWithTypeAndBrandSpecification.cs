using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications.ProductSpecification
{
    public class ProductWithTypeAndBrandSpecification:BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification()
        {
            AddInclude(x=> x.ProductType);
            AddInclude(x=> x.ProductBrand);            
        }
        
    }
}