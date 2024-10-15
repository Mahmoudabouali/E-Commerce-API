using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketReposiory
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);
        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket,TimeSpan? timeToLive = null); //ttl
        public Task<bool> DeleteBasketAsync(string id);
    }
}
