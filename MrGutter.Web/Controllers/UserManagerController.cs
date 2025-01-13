using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services;
using MrGutter.Services.IService;
using Newtonsoft.Json;
using System.Runtime.ExceptionServices;

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

            var allRole = await _userManagerService.GetRoleAsync("");

            var roles = allRole.Roles.ToList();


            // Map roles dynamically to a list of SelectListItem
            var rolesDropDown = roles.Select(role => new SelectListItem
            {
                Text = role.RoleName, 
                Value = role.RoleID.ToString() 
            }).ToList();

            var CompanyList = new List<SelectListItem>
            {
                new SelectListItem { Text = "MrGutter", Value = "1" },
                new SelectListItem { Text = "US Metal Roofing", Value = "2" }
            };

            ViewBag.Company = CompanyList;
            ViewBag.Roles = rolesDropDown;
            return View();
        }
        [HttpPost]        
        public async Task<IActionResult> UserList()
        {
            var result = await _userManagerService.GetUserAsync(""); // Fetch users
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
                users = users.Where(m =>
                    (m.FirstName + " " + m.LastName).Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Email.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Mobile.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.UserType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.UserStatus.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            // Count filtered records
            int recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize)
                            .Select(user => new {
                                userName = user.FirstName + " " + user.LastName,
                                email = user.Email,
                                mobile = user.Mobile,
                                userType = user.UserType,
                                userStatus = user.UserStatus,
                                userID = user.UserID
                            }).ToList();

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalUserList.Count(), data = data };
            return Ok(jsonData);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(UsersVM user)
        {
            var res = await _userManagerService.DeleteUser(user);

            return View("User");

        }

        // GET method to fetch user details for editing
        [HttpGet]
        public async Task<IActionResult> EditUser(string? userID)
        {
            var user1 = await _userManagerService.GetUserAsync(userID);
            //UsersVM user = JsonConvert.DeserializeObject<UsersVM>(user1.ToString());

            var userType = user1.Users.FirstOrDefault(m => m.UserID == userID);
            int IntegerUserId = Int32.Parse(userID);

            var allRole = await _userManagerService.GetRoleAsync("");

            // var roleId = role.Roles.FirstOrDefault(m => m.RoleName == userType.UserType);
            var roles = allRole.Roles.Select(m => new { RoleID = m.RoleID, RoleName = m.RoleName }).ToList();

            var passRoleId = roles.FirstOrDefault(m => m.RoleName.ToString() == userType.UserType);

            var user = user1.Users.FirstOrDefault(m => m.UserID == userID);
            if (user == null)
            {
                return NotFound();
            }

            //var userVM = new UsersVM
            //{
            //    UserID = userID,
            //    RoleID = user.RoleID,
            //    UserName = user.UserName,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    Mobile = user.Mobile,
            //    UserType = user.UserType,
            //    UserStatus = user.UserStatus,
            //    isActive = user.isActive
            //};
            // Return the response with roles and user data
            return Json(new
            {
                UserID = userID,
                RoleID = passRoleId.RoleID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Mobile = user.Mobile,
                UserType = user.UserType,
                UserStatus = user.UserStatus,
                isActive = user.isActive,
                Roles = roles // Correctly defined roles here
            });  // Return user data as JSON
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UsersVM user)
        {
            //var result = await _userManagerService.GetUserAsync(user.UserID);
            //var res1 = result.Users.FirstOrDefault(m => m.UserID == user.UserID);
            //UsersVM data = new UsersVM
            //{
            //    FirstName = res1?.FirstName,
            //    LastName = res1?.LastName,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    MobileNo = user.MobileNo,
            //    UserStatus = user.UserStatus,
            //    UserType = user.UserType,
            //    UserID = user.UserID
            //};

            var res = await _userManagerService.CreateOrUpdateUser(user);
            //var res = user.Users.FirstOrDefault(m => m.UserID == );

            return View(res);
        }


        [HttpPost]
        public async Task<ActionResult> CreateUser(UsersVM user)
        {

            var res = await _userManagerService.CreateOrUpdateUser(user);
            //var res = user.Users.FirstOrDefault(m => m.UserID == );

            return View("User");
        }
        public async Task<ActionResult> Company()
        {
           // var result = await _userManagerService.GetCompanyAsync("");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompanyList()
        {
            var result = await _userManagerService.GetCompanyAsync(""); // Fetch Company
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            // Store the original user list for count
            var originalCompanyList = result.Company.ToList(); // Make sure to evaluate the list here
            var users = originalCompanyList.AsQueryable();

            // Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                bool descendingOrder = sortColumnDirection == "asc" ? false : true;
                users = users.OrderByProperty(sortColumn, descendingOrder);
            }

            // Searching
            if (!string.IsNullOrEmpty(searchValue))
            {
                users = users.Where(m =>
                    m.CompanyName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.ContactPerson.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.CompanyEmail.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.CompanyPhone.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            // Count filtered records
            int recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize)
                            .Select(user => new {
                                companyName = user.CompanyName,
                                companyEmail = user.CompanyEmail,
                                companyPhone = user.CompanyPhone,
                                contactPerson = user.ContactPerson,
                                companyId = user.CompanyId
                            }).ToList();

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalCompanyList.Count(), data = data };
            return Ok(jsonData);
        }



        [HttpGet]
        public async Task<IActionResult> EditCompany(string? companyId)
        {
            var user1 = await _userManagerService.GetCompanyAsync(companyId);
            var res = user1.Company.FirstOrDefault(m => m.CompanyId == companyId);
            


            return Json(new
            {
                companyId = res.CompanyId,
                companyName = res.CompanyName,
                companyEmail = res.CompanyEmail,
                companyPhone = res.CompanyPhone,
                contactPerson = res.ContactPerson,
            });  // Return user data as JSON
        }

        [HttpPost]
        public async Task<ActionResult> EditCompany(CompanyVM compInfo)
        {
            

            var res = await _userManagerService.CreateOrUpdateCompany(compInfo);
            //var res = user.Users.FirstOrDefault(m => m.UserID == );

            return View(res);
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
