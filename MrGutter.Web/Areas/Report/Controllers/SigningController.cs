using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Report.Controllers
{
    [Area("Report")]
    public class SigningController : Controller
    {
        public IActionResult CustomerReview()
        {
            return View();
        }
    }
}
