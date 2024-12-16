using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Template.Controllers
{
    [Area("Template")]
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
