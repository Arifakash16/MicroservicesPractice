using Basket.API.Models;

namespace Basket.API.Repositories
{
    public interface IBasketRepo
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasKet(string userName);
    }
}
