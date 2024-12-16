using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Controllers
{
    public class ConfigurationController : Controller
    {
     
        public IActionResult Branding()
        {
            return View();
        }
    }
}
