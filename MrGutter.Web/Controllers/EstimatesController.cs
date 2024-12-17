using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Controllers
{
    public class EstimatesController : Controller
    {
        public IActionResult EstimateList()
        {
            return View();
        }
        public IActionResult EstimationDetails()
        {
            return View();
        }
        public IActionResult EstimationSettings()
        {
            return View();
        }
    }
}
