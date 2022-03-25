using Microsoft.AspNetCore.Mvc;

namespace VideoGamesShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
