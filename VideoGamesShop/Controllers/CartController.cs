using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Extensions;

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
            if (User.Id() != userId)
            {
                return RedirectToAction("404", "Error");
            }
            var productsInCart = await cartService.UsersCart(userId);

            return View(productsInCart);
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(string userId, string gameId)
        {
            await cartService.RemoveFromCart(userId, gameId);

            return RedirectToAction("MyCart", "Cart", new { userId = userId});
        }

        public async Task<IActionResult> Buy(string userId)
        {
            await cartService.BuyProductsInCart(userId);

            // return RedirectToAction("MyGames", "Library", new { userId = userId });
            return Redirect("/");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
