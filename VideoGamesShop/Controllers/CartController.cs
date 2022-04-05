using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> MyCart(string userId)
        {
            var productsInCart = await cartService.UsersCart(userId);

            return View(productsInCart);
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(string userId, string gameId)
        {
            await cartService.RemoveFromCart(userId, gameId);

            return RedirectToAction("MyCart", "Cart", new { userId = userId});
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
