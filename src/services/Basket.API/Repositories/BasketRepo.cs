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

        public async Task<ShoppingCard> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);

            if(string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert
        }

        public Task<ShoppingCard> UpdateBasket(ShoppingCard basket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBasKet(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
