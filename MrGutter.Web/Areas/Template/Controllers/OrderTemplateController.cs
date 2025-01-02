using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Layout.Controllers
{
    [Area("Template")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class OrderTemplateController : Controller
    {
        public IActionResult OrderLayoutList()
        {
            return View();
        }
        public IActionResult LayoutTitle(string? layoutId)
        {
            return View();
        }
        public IActionResult ScopeOfWork(string? layoutId)
        {
            return View();
        }
        //EditOrderLayoutInspection.cshtml
        public IActionResult EditOrderLayoutInspection(string? layoutId)
        {
            return View();
        }
        public IActionResult Materials(string? layoutId)
        {
            return View();
        }
    }
}
