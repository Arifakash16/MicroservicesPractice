using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepo(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if(string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);

        }

        public async Task DeleteBasKet(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
