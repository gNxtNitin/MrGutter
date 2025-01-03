using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;

namespace MrGutter.Web.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UserManagerController : Controller
    {
        [HttpGet]
        public IActionResult User()
        {
            User users = new User();
            


            return View();
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult MyProfile()
        {
            return View();
        }
        public IActionResult EditPassword()
        {
            return View();
        }
    }
}
