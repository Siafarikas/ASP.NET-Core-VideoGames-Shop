using Microsoft.AspNetCore.Mvc;

namespace VideoGamesShop.Areas.Developer.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
