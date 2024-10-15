using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        public Task<BasketDTO?> GetBasketAsync(string id);
        public Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
