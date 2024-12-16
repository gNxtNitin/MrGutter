using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    public class EmailSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
