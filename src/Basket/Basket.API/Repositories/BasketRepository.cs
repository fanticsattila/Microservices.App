﻿using Basket.API.Data.Interfaces;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            return await _context.Redis.KeyDeleteAsync(userName);
        }

        public async Task<BasketCart> GetBasket(string userName)
        {

            RedisValue basket = await _context.Redis.StringGetAsync(userName);
            if (basket.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            bool updated = await _context.Redis.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            if (!updated)
                return null;
            return await GetBasket(basket.UserName);
        }
    }
}