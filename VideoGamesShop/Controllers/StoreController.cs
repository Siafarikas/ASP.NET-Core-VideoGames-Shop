using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Constants;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Game;

namespace VideoGamesShop.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ICartService cartService;
        private readonly IWishlistService wishlistService;

        public StoreController(
            IGameService _gameService,
            ICartService _cartService,
            IWishlistService _wishlistService)
        {
            gameService = _gameService;
            cartService = _cartService;
            wishlistService = _wishlistService;
        }

        [Authorize]
        public async Task<IActionResult> Games(int pg = 1)
        {
            var games = await gameService.GetGames();

            const int pageSize = 6;

            if (pg < 1)
            {
                pg = 1;
            }

            int recsCount = games.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = games.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(string userId, string gameId)
        {
            var addedToCart = await cartService.AddToCart(userId, gameId);

            if (addedToCart == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully added to cart!";
                return RedirectToAction("MyCart", "Cart", new { userId = userId });
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred";
                return RedirectToAction("Games", "Store");
            }
        }

        [Authorize]
        public async Task<IActionResult> AddToWishlist(string userId, string gameId)
        {
            var addedToCart = await wishlistService.AddToWishlist(userId, gameId);

            if (addedToCart == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully added to wishlist!";
                return RedirectToAction("MyWishlist", "Wishlist", new { userId = userId });
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred";
                return RedirectToAction("Games", "Store");
            }
        }


        [Authorize]
        public async Task<IActionResult> GameDetails(string gameId)
        {
            if (!(await gameService.GameWithIdExists(gameId)))
            {
                return RedirectToAction("404", "Error");
            }
            var game = await gameService.GameDetails(gameId);

            return View(game);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
