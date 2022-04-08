using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Contracts;

namespace VideoGamesShop.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ICartService cartService;

        public StoreController(
            IGameService _gameService,
            ICartService _cartService)
        {
            gameService = _gameService;
            cartService = _cartService;
        }

        [Authorize]
        public async Task<IActionResult> Games()
        {
            var games = await gameService.GetGames();

            return View(games);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(string userId, string gameId)
         {
            var addedToCart = await cartService.AddToCart(userId, gameId);

            if (addedToCart == true)
            {
                //TempData[GlobalMessageKey] = "Successfully added to cart!";
            }
            else
            {
                //TempData[GlobalMessageKey] = "An error accured";
            }
            return RedirectToAction("MyCart", "Cart", new {userId = userId});
        }

        [Authorize]
        public async Task<IActionResult> AddToWishlist()
        {

            return View();
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
