using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Template.Controllers
{
    [Area("Template")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class QuickTextTemplateController : Controller
    {
        public IActionResult QuickTextTemplateList()
        {
            return View();
        }
        public IActionResult QuickText(string? introTempID)
        {
            return View();
        }
    }
}
