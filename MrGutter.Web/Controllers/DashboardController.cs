using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Services.IService;

namespace MrGutter.Web.Controllers
{
    //[Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class DashboardController : Controller
    {
        private readonly IAccountService _accountService;
        public DashboardController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
