using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Report.Controllers
{
    [Area("Report")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class SigningController : Controller
    {
        public IActionResult CustomerReview()
        {
            return View();
        }
    }
}
