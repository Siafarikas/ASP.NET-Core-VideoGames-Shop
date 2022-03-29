using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGamesShop.Core.Constants;

namespace VideoGamesShop.Areas.Developer.Controllers
{
    [Authorize(Roles = UserConstants.Roles.Developer)]
    [Area("Developer")]
    public class BaseController : Controller
    {

    }
}
