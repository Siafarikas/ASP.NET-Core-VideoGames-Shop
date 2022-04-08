using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Extensions;
using VideoGamesShop.Infrastructure.Data.Identity;

namespace VideoGamesShop.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        private readonly IGameService gameService;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _userService,
            IGameService _gameService
            )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
            gameService = _gameService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            ApplicationUser applicationUser = await userManager.GetUserAsync(User);

            return View();
        }

        public async Task<IActionResult> Profile(string userId)
        {
            if (User.Id() != userId)
            {
                return RedirectToAction("404", "Error");
            }
            var user = await userService.GetUserProfileInfo(userId);

            return View(user);
        }

        public async Task<IActionResult> MyWallet(string userId)
        {
            if (User.Id() != userId)
            {
                return RedirectToAction("404", "Error");
            }
            var user = await userService.GetUserById(userId);

            return View(user);
        }

        public async Task<IActionResult> AddMoneyToWallet(string userId, decimal amount)
        {
            await userService.AddMoneyToWallet(userId, amount);

            return RedirectToAction("MyWallet", "User", new { userId = userId });
        }

        public async Task<IActionResult> MyLibrary(string userId)
        {
            if (User.Id() != userId)
            {
                return RedirectToAction("404", "Error");
            }
            var games = await gameService.GetUsersGames(userId);
            return View(games);
        }

        /*public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Developer"
            });

            return Ok();
        }*/
    }
}
