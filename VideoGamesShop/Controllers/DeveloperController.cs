using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Constants;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Core.Models.Game;
using VideoGamesShop.Extensions;
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
            var userId = User.Id();
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

            if (gameCreated == true)
            {
                TempData[MessageConstants.SuccessMessage] = "Successfully published game!";
            }
            else
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
            }

            return Redirect("~/store/games");
        }

        public async Task<IActionResult> Dashboard(string userId)
        {
            var model = await userService.GetStatistics(userId);
            if (model == null)
            {
                TempData[MessageConstants.ErrorMessage] = "An error occurred!";
            }
            return View(model);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
