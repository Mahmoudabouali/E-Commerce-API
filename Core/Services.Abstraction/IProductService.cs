using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        // get all products
        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(string? sort, int? brandId, int? typeId);
        // get all brands 
        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        // get all types
        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();
        // get product by Id
        public Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
