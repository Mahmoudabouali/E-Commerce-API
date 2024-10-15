using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper, IBasketReposiory basketReposiory)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketService>(() => new BasketService(basketReposiory, mapper));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
