using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    public class PageSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
