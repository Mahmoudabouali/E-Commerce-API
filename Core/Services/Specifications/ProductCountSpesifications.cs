using Domain.Contracts;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpesifications : Specifications<Product>
    {
        public ProductCountSpesifications(ProductSpecificationPrameters prameters)
            :base(product => 
            (!prameters.BrandId.HasValue || product.BrandId == prameters.BrandId.Value) &&
            (!prameters.TypeId.HasValue || product.TypeId == prameters.TypeId.Value) &&
            (string.IsNullOrWhiteSpace(prameters.Search) || product.Name.ToLower().Contains(prameters.Search.ToLower().Trim())))
        {
            
        }
    }
}
