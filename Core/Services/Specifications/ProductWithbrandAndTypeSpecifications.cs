using Domain.Contracts;
using Domain.Entities;
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
        public ProductWithbrandAndTypeSpecifications(string? sort, int? brandId,int? typeId) 
            : base(product => 
            (!brandId.HasValue || product.BrandId == brandId.Value)&&
            (!typeId.HasValue || product.TypeId == typeId.Value))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim()) 
                {
                    case "pricedesc":
                        SetOrderByDescending(p => p.Price);
                        break;
                    case "priceasc":
                        SetOrderBy(p => p.Price);
                        break;
                    case "namedesc":
                        SetOrderByDescending(p => p.Name);
                        break;
                    default:
                        SetOrderBy(p => p.Name);
                        break;
                }
            }



        }
    }
}
