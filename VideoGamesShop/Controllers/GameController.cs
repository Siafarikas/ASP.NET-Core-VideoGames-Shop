using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Contracts;

namespace VideoGamesShop.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService gameService;

        public GameController(IGameService _gameService)
        {
            gameService = _gameService;
        }
        public async Task<IActionResult> Store()
        {
            var games = await gameService.GetGames();

            return View(games);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
