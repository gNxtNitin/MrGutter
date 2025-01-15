using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services;
using MrGutter.Services.IService;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace MrGutter.Web.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class EstimatesController : Controller
    {
        private readonly IEstimatesService _estimatesService;
        private readonly IUserManagerService _userManagerService;
        private readonly IAccountService _accountService;
        public  EstimatesController(IEstimatesService estimatesService, IAccountService accountService, IUserManagerService userManagerService)
        {
            _estimatesService = estimatesService;
            _accountService = accountService;
            _userManagerService = userManagerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateEstimate(EstimateVM estimateVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EstimateError = true;
                return RedirectToAction("EstimateList");
            }
            estimateVM.CreatedBy = HttpContext.Session.GetInt32("UserId") ?? 0;
            var result = await _estimatesService.CreateEstimateAsync(estimateVM);
          
           
            return RedirectToAction("EstimateList");
        }
        public async Task<IActionResult> EstimateList()
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0  ;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            estimateIdsVM.UserId = userId;

            EstimateVM estimateVM = new EstimateVM();
            var StatusColor = await _estimatesService.GetStatuslistAsync("");
            estimateVM =await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator"); 
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();

            estimateVM.EstimatorUsers = estimatorUsers;
            estimateVM.StatusList = StatusColor.StatusList;
            return View(estimateVM);   
        }
        //For grid estimate list
        [HttpPost]
        public async Task<IActionResult> EstimateData()
        {
            
            EstimateIdsVM estimateFilterReq = new EstimateIdsVM();
            estimateFilterReq.CompanyID = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            estimateFilterReq.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var result = await _estimatesService.GetEstimatelistAsync(estimateFilterReq); 
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            

            var originalEstimateList = result.EstimateList.ToList(); // Make sure to evaluate the list here
            var estimateList = originalEstimateList.AsQueryable();
            

            var AllStatus = await _estimatesService.GetStatuslistAsync("");
            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();


            // Sorting
            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //{
            //    bool descendingOrder = sortColumnDirection == "asc" ? false : true;
            //    estimateList = estimateList.OrderByProperty(sortColumn, descendingOrder);
            //}

            // Searching
            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    estimateList = estimateList.Where(m =>
            //        (m.FirstName + " " + m.LastName).Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
            //        m.Email.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
            //        m.EstimateNo.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
            //        m.Addressline1.Contains(searchValue, StringComparison.OrdinalIgnoreCase) 

            //}

            int recordsTotal = estimateList.Count();
            var data = estimateList
                            .Select(estimate => new {
                                id = estimate.EstimateID,
                                est_no = estimate.EstimateNo,
                                est_name = estimate.FirstName+" "+ estimate.LastName,
                                address = estimate.Addressline1,
                                salesperson = estimate.FirstName,
                                created = estimate.EstimateCreatedDate,
                                status = AllStatus.StatusList.Where (x=>x.StatusID == estimate.StatusID.ToString()).Select(x=>x.StatusName).FirstOrDefault()
                            }).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalEstimateList.Count(), data = data };
            return Ok(jsonData);
        }
        [HttpPost]
        public async Task<IActionResult> GetEstimateList()
        {

            EstimateVM estimateVM = new EstimateVM();
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            estimateIdsVM.UserId = userId;
            estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);

            //var estimate = estimates.EstimateList.FirstOrDefault(m => m.EstimateID  == estimateID);
            return View();
        }

        public async Task<IActionResult> EstimationDetails(int id)
        {
            EstimateVM estimateVM = new EstimateVM();
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            estimateIdsVM.UserId = userId;
            estimateIdsVM.EstimateID = id;
            estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
            return View(estimateVM);
        }
    

        public async Task<IActionResult> EstimationSettings(int EstimateId)
        {
            EstimateVM estimateVM = new EstimateVM();
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            estimateIdsVM.UserId = userId;
            estimateIdsVM.EstimateID = EstimateId;
            var StatusColor = await _estimatesService.GetStatuslistAsync("");
            estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();
            estimateVM.EstimatorUsers = estimatorUsers;
            return View(estimateVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEstimate(int estimateId)
        {
            EstimateVM estimateVM = new EstimateVM();
            estimateVM.EstimateID = estimateId;
            var result = await _estimatesService.DeleteEstimateAsync(estimateVM);
            return RedirectToAction("EstimateList");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEstimate(EstimateVM estimateVM)
        {
            
            var result = await _estimatesService.UpdateEstimate(estimateVM);
            return RedirectToAction("EstimateList");
        }
        //[HttpPost]
        //public async Task<IActionResult> GetEstimatesList()
        //{
        //    var result = await _estimatesService.GetEstimatelistAsync (""); 
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = Request.Form["start"].FirstOrDefault();
        //    var length = Request.Form["length"].FirstOrDefault();
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var originalCompanyList = result.EstimateList.ToList();
        //    var users = originalCompanyList.AsQueryable();

        //    // Sorting
        //    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //    {
        //        bool descendingOrder = sortColumnDirection == "asc" ? false : true;
        //        users = users.OrderByProperty(sortColumn, descendingOrder);
        //    }

        //    // Searching
        //    if (!string.IsNullOrEmpty(searchValue))
        //    {
        //        users = users.Where(m =>
        //            m.EstimateNo.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //            m.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //            m.Addressline1.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //            m.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //            m.EstimateCreatedDate.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
        //    }

        //    // Count filtered records
        //    int recordsTotal = users.Count();
        //    var data = users.Skip(skip).Take(pageSize)
        //                    .Select(user => new {
        //                        estimateNo = user.EstimateNo,
        //                        firstName = user.FirstName,
        //                        addressline1 = user.Addressline1,
        //                        lastName = user.LastName,
        //                        estimateCreatedDate = user.EstimateCreatedDate,
        //                        estimateId = user.EstimateID
        //                    }).ToList();

        //    var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalCompanyList.Count(), data = data };
        //    return Ok(jsonData);
        //}

        [HttpPost]
        public async Task<IActionResult> ChangeEstimateStatus(int statusId , int estimateId)
        {
            EstimateVM estimateVM = new EstimateVM();
            estimateVM.EstimateID = estimateId;
            estimateVM.StatusID = statusId;
          
            var result = await _estimatesService.ChangeEstimateStatus(estimateVM);
            return RedirectToAction("EstimateList");
        }
    }
}
