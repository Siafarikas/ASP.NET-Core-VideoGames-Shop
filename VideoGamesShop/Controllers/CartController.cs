using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Constants;
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
            var removedItem = await cartService.RemoveFromCart(userId, gameId);

            if (removedItem == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully removed from cart!";
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
            }

            return RedirectToAction("MyCart", "Cart", new { userId = userId });
        }

        public async Task<IActionResult> Buy(string userId)
        {
            var boughtItems = await cartService.BuyProductsInCart(userId);

            if (boughtItems == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successful purchase!";
                return RedirectToAction("MyLibrary", "User", new { userId = userId });
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
                return RedirectToAction("MyCart", "Cart", new { userId = userId });
            }
        }

        public async Task<IActionResult> MoveToWishlist(string userId, string gameId)
        {
            var movedToWishlist = await cartService.MoveToWishlist(userId, gameId);

            if (movedToWishlist == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully moved to wishlist!";
                return RedirectToAction("MyWishlist", "Wishlist", new { userId = userId });
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "Game already in wishlist!";
                return RedirectToAction("MyCart", "Cart", new { userId = userId });
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
