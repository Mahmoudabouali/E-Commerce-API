using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationPrameters
    {
        private const int MAXBAGESIZE = 10;
        private const int DEFAULTBAGESIZE = 5;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions? sort { get; set;}
        public int PageIndex { get; set; } = 1;
        private int _pageSize = DEFAULTBAGESIZE;
        public string? Search {  get; set; }

        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = value > MAXBAGESIZE ? MAXBAGESIZE : value;
        }

    }
    public enum ProductSortingOptions
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc
    }
}
