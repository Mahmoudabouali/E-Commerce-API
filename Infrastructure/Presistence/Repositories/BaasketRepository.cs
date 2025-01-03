﻿using Domain.Contracts;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        : IBasketReposiory
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
            => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);
            if(value.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(value);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpadeted = await _database
                .StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(30));
            return isCreatedOrUpadeted ? await GetBasketAsync(basket.Id) : null;    
        }
    }
}
