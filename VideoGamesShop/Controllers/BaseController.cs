using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VideoGamesShop.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
