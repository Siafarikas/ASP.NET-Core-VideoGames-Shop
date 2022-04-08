using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Game;
using static VideoGamesShop.Core.Constants.UserConstants;

namespace VideoGamesShop.Controllers
{
    [Authorize(Roles = Roles.Developer)]
    public class DeveloperController : BaseController
    {

        private readonly IGameService gameService;
        private readonly IUserService userService;

        public DeveloperController(
            IGameService _gameService,
            IUserService _userService)
        {
            gameService = _gameService;
            userService = _userService;
        }

        [Authorize]
        public async Task<IActionResult> AddGame()
        {

            return View(new AddGameFormModel
            {
                Genres = await gameService.GetAllGenres()
            });

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddGame(AddGameFormModel game)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string developerId = await userService.GetDeveloperIdByUserId(userId);

            var gameCreated = await gameService.AddGame(
                game.Title,
                game.GenreId,
                game.Price,
                game.ReleaseDate,
                game.Description,
                game.ImageUrl,
                developerId
                );

            //TempData[GlobalMessageKey] = "Successfully published game!";

            return Redirect("~/store/games");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
