using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Constants;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Extensions;

namespace VideoGamesShop.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService wishlistService;

        public WishlistController(IWishlistService _wishlistService)
        {
            wishlistService = _wishlistService;
        }

        [Authorize]
        public async Task<IActionResult> MyWishlist(string userId)
        {
            if (User.Id() != userId)
            {
                return RedirectToAction("404", "Error");
            }
            var games = await wishlistService.UsersWishlist(userId);
            return View(games);
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromWishlist(string userId, string gameId)
        {
            var removedGame = await wishlistService.RemoveFromWishlist(userId, gameId);

            if (removedGame == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully removed from wishlist!";
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
            }

            return RedirectToAction("MyWishlist", "Wishlist", new { userId = userId });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
