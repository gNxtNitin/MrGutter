using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
