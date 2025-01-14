using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services;
using MrGutter.Services.IService;

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
            var result = await _estimatesService.CreateEstimateAsync(estimateVM);
          
           
            return RedirectToAction("EstimateList");
        }
        public async Task<IActionResult> EstimateList()
        {
           EstimateVM estimateVM = new EstimateVM();
            var StatusColor = await _estimatesService.GetStatuslistAsync("");
            estimateVM =await _estimatesService.GetEstimatelistAsync("");
            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator"); 
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();

            estimateVM.EstimatorUsers = estimatorUsers;
            estimateVM.StatusList = StatusColor.StatusList;
            return View(estimateVM);   
        }
        [HttpPost]
        public async Task<IActionResult> GetEstimateList()
        {
            var estimateID = 0;
            var estimates = await _estimatesService.GetEstimatelistAsync("");
            var estimate = estimates.EstimateList.FirstOrDefault(m => m.EstimateID  == estimateID);
            return View();
        }

        public IActionResult EstimationDetails(int id)
        {
            
            return View();
        }
        public async Task<IActionResult> EstimationSettings()
        {
            var allRole = await _userManagerService.GetUserAsync("");
            EstimateVM estimateVM = new EstimateVM();
           
            return View();
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
        public async Task<IActionResult> GetEstimatesList()
        {
            var result = await _estimatesService.GetEstimatelistAsync (""); 
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            var originalCompanyList = result.EstimateList.ToList();
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
                    m.EstimateNo.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.Addressline1.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                    m.EstimateCreatedDate.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            // Count filtered records
            int recordsTotal = users.Count();
            var data = users.Skip(skip).Take(pageSize)
                            .Select(user => new {
                                estimateNo = user.EstimateNo,
                                firstName = user.FirstName,
                                addressline1 = user.Addressline1,
                                lastName = user.LastName,
                                estimateCreatedDate = user.EstimateCreatedDate,
                                estimateId = user.EstimateID
                            }).ToList();

            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = originalCompanyList.Count(), data = data };
            return Ok(jsonData);
        }

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
