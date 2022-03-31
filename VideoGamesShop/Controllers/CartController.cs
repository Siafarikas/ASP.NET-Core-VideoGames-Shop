using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Contracts;

namespace VideoGamesShop.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService cartService;

        public CartController(ICartService _cartService)
        {
            cartService = _cartService;
        }

        public async Task<IActionResult> MyCart(string userId)
        {
            var productsInCart = await cartService.UsersCart(userId);

            return View(productsInCart);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
