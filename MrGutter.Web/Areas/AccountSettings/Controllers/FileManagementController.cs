using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    public class FileManagementController : Controller
    {
        public IActionResult AccountSettingsPDFs()
        {
            return View();
        }
    }
}
