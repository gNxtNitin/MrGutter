using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Controllers
{
    public class BrandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
