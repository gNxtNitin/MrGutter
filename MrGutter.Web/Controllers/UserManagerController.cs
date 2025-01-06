using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services;
using MrGutter.Services.IService;
using Newtonsoft.Json;

namespace MrGutter.Web.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UserManagerController : Controller
    {
        private readonly IUserManagerService _userManagerService;
        public UserManagerController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        public async Task<IActionResult> User()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserList()
        {
            var result = await _userManagerService.GetUserAsync("");
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            // Store the original user list for count
            var originalUserList = result.Users.ToList(); // Make sure to evaluate the list here
            var users = originalUserList.AsQueryable();

            // Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                bool descendingOrder = sortColumnDirection == "asc" ? false : true;
                users = users.OrderByProperty(sortColumn, descendingOrder);
            }

            // Searching
            if (!string.IsNullOrEmpty(searchValue))
            {
                users = users.Where(m => m.UserName.ToString().Contains(searchValue)
                                                    || m.Email .Contains(searchValue)
                                                      || m.Mobile.Contains(searchValue)
                                                     || m.UserType.Contains(searchValue)
                                                    || m.UserStatus.Contains(searchValue));


            }
            // Count filtered records
            int recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize).ToList();

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalUserList.Count(), data = users };
            return Ok(jsonData);

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
        [HttpPost]
        public IActionResult EditPassword()
        {
            return View();
        }
    }
}
