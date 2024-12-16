using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    public class TaxSettingsController : Controller
    {
        public IActionResult AccountTaxSettings()
        {
            return View();
        }
    }
}
