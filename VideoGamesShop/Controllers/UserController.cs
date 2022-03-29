using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VideoGamesShop.Core.Contracts;
using VideoGamesShop.Infrastructure.Data.Identity;

namespace VideoGamesShop.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService userService;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _userService
            )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            userService = _userService;
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

        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Developer"
            });

            return Ok();
        }
    }
}
