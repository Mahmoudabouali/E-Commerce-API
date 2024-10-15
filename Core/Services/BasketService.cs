using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketReposiory basketReposiory ,IMapper mapper) 
        : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
            => await basketReposiory.DeleteBasketAsync(id);

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
            var basket = await basketReposiory.GetBasketAsync(id);
            
            return basket is null ? throw new BasketNotFoundExeption(id) 
                : mapper.Map<BasketDTO>(basket);
        }

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var updatedBasket = await basketReposiory.UpdateBasketAsync(customerBasket);

            return updatedBasket is null ? 
                throw new Exception("can't update basket now!!")
                :mapper.Map<BasketDTO>(updatedBasket);
        }
    }
}
