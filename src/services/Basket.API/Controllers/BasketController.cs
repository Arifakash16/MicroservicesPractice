using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepo _basketRepo;

        public BasketController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            try
            {
                var basket = await _basketRepo.GetBasket(userName);
                return CustomResult("Basket data load successfully", basket);
            }
            catch(Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBaket([FromBody] ShoppingCart basket)
        {
            try
            {
                return CustomResult("Basket modified done", await _basketRepo.UpdateBasket(basket));
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBaket(string userName)
        {
            try
            {
                await _basketRepo.DeleteBasKet(userName);
                return CustomResult("Basket has been deleted");
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
