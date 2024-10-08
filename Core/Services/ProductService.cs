using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class ProductService(IUnitOfWork unitOfWork ,IMapper mapper) : IProductService
    {
        
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            //1. retrive all brands => unitofwork
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //2. map to brand ResultDto => IMapper
            var brandsResult = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            // return
            return brandsResult;
        }
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId)
        {
            var products = await unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithbrandAndTypeSpecifications(sort , brandId, typeId));
            var productsResult = mapper.Map<IEnumerable<ProductResultDto>>(products);
            return productsResult;
        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesResult = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return TypesResult;
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product,int>().GetAsync(new ProductWithbrandAndTypeSpecifications(id));
            var productsResult = mapper.Map<ProductResultDto>(product);
            return productsResult;
        }
    }
}
