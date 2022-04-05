using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Contracts;
using static VideoGamesShop.Core.Constants.UserConstants;

namespace VideoGamesShop.Controllers
{
    [Authorize(Roles = Roles.Developer)]
    public class DeveloperController : BaseController
    {

        private readonly IGameService gameService;

        public DeveloperController(IGameService _gameService)
        {
            gameService = _gameService;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
