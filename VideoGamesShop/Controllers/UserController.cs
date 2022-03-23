using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace VideoGamesShop.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Administrator"
            //});

            return Ok();
        }
    }
}
