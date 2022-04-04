using Microsoft.AspNetCore.Mvc;

namespace VideoGamesShop.Controllers
{
    [Route("/Error/{statusCode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["Error"] = "Page Not Found";
                    break;

                default:
                    break;
            }
            return View("PageNotFound");
        }
    }
}
