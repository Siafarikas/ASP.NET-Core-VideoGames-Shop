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

        public async Task<IActionResult> RemoveFromCart(string userId, string gameId)
        {
            await cartService.RemoveFromCart(userId, gameId);

            return Redirect("~/cart/mycart");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
