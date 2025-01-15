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

            var allCompanies = await _userManagerService.GetCompanyAsync("");
            var companies = allCompanies.Company.ToList();

            var rolesDropDown = roles.Select(role => new SelectListItem
            {
                Text = role.RoleName, 
                Value = role.RoleID.ToString() 
            }).ToList();

            var CompanyList = companies.Select(cmp => new SelectListItem
            {
                Text = cmp.CompanyName,
                Value = cmp.CompanyId.ToString()
            }).ToList();
            ViewBag.Company = CompanyList;
            ViewBag.Roles = rolesDropDown;
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

            var originalUserList = result.Users.ToList(); 
            var users = originalUserList.AsQueryable();

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                bool descendingOrder = sortColumnDirection == "asc" ? false : true;
                users = users.OrderByProperty(sortColumn, descendingOrder);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                users = users.Where(m =>
                    (m.FirstName + " " + m.LastName).Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Email.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Mobile.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.UserType.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.UserStatus.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

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
            user.CreatedBy = HttpContext.Session.GetInt32("UserId").ToString();
            var res = await _userManagerService.DeleteUser(user);

            return View("User");

        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string? userID)
        {
            var user1 = await _userManagerService.GetUserAsync(userID);

            var userType = user1.Users.FirstOrDefault(m => m.UserID == userID);
            int IntegerUserId = Int32.Parse(userID);

            var allRole = await _userManagerService.GetRoleAsync("");
            var roles = allRole.Roles.Select(m => new { RoleID = m.RoleID, RoleName = m.RoleName }).ToList();
            var passRoleId = roles.FirstOrDefault(m => m.RoleName.ToString() == userType.UserType);           
            var user = user1.Users.FirstOrDefault(m => m.UserID == userID);
            if (user == null)
            {
                return NotFound();
            }
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
                Roles = roles 
            });  
        }

        [HttpGet]
        public async Task<IActionResult> UserView(string? userID)
        {
            var user1 = await _userManagerService.GetUserAsync(userID);

            var userType = user1.Users.FirstOrDefault(m => m.UserID == userID);
            int IntegerUserId = Int32.Parse(userID);

            var allRole = await _userManagerService.GetRoleAsync("");
            var roles = allRole.Roles.Select(m => new { RoleID = m.RoleID, RoleName = m.RoleName }).ToList();
            var passRoleId = roles.FirstOrDefault(m => m.RoleName.ToString() == userType.UserType);
            var user = user1.Users.FirstOrDefault(m => m.UserID == userID);
            if (user == null)
            {
                return NotFound();
            }
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
                Roles = roles
            });
        }



        [HttpPost]
        public async Task<ActionResult> EditUser(UsersVM user)
        {
            user.CompanyId = HttpContext.Session.GetInt32("CompanyId").ToString();
            user.CreatedBy = HttpContext.Session.GetInt32("UserId").ToString();
            var res = await _userManagerService.CreateOrUpdateUser(user);
            return View("User");
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(UsersVM user)
        {
            user.CreatedBy = HttpContext.Session.GetInt32("UserId").ToString();
            var res = await _userManagerService.CreateOrUpdateUser(user);
            return View("User");
        }
        public async Task<ActionResult> Company()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompanyList()
        {
            var result = await _userManagerService.GetCompanyAsync(""); 
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var originalCompanyList = result.Company.ToList(); 
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
            });  
        }

        [HttpPost]
        public async Task<ActionResult> EditCompany(CompanyVM compInfo)
        {
            compInfo.CreatedBy = HttpContext.Session.GetInt32("UserId");
            var res = await _userManagerService.CreateOrUpdateCompany(compInfo);
            return View(res);
        }
        [HttpPost]
        public async Task<ActionResult> CreateCompany(CompanyVM cmpInfo)
        {
            cmpInfo.CreatedBy = HttpContext.Session.GetInt32("UserId");
            var res = await _userManagerService.CreateOrUpdateCompany(cmpInfo);
            return View("Company");
        }

        public async Task<ActionResult> DeleteCompany(CompanyVM cmp)
        {
            cmp.CreatedBy = HttpContext.Session.GetInt32("UserId");
            var res = await _userManagerService.DeleteCompanyAsync(cmp);
            return View("Company");
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
