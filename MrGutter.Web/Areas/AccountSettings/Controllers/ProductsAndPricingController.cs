using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.AccountSettings.Controllers
{
    [Area("AccountSettings")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ProductsAndPricingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductsAndPricing()
        {
            return View();
        }
        public IActionResult ProfitSettings()
        {
            return View();
        }

        public IActionResult DefaultProductList()
        {
            return View();
        }
        public IActionResult EditPriceList()
        {
            return View();
        }
        public IActionResult AfterEditPriceList()
        {
            return View();
        }




    }
}
