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
    internal class ProductWithbrandAndTypeSpecifications : Specifications<Product>
    {
        // use to retrieve product by id 
        public ProductWithbrandAndTypeSpecifications(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }
        //use to get all products
        public ProductWithbrandAndTypeSpecifications(ProductSpecificationPrameters prameters) 
            : base(product => 
            (!prameters.BrandId.HasValue || product.BrandId == prameters.BrandId.Value)&&
            (!prameters.TypeId.HasValue || product.TypeId == prameters.TypeId.Value)&&
            (string.IsNullOrWhiteSpace(prameters.Search) || product.Name.ToLower().Contains(prameters.Search.ToLower().Trim())))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            ApplyPagination(prameters.PageIndex,prameters.PageSize);
            if (prameters.sort is not null)
            {
                switch (prameters.sort) 
                {
                    case ProductSortingOptions.PriceDesc:
                        SetOrderByDescending(p => p.Price);
                        break;
                    case ProductSortingOptions.PriceAsc:
                        SetOrderBy(p => p.Price);
                        break;
                    case ProductSortingOptions.NameDesc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    case ProductSortingOptions.NameAsc:
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
