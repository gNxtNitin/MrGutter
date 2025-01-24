using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services;
using MrGutter.Services.IService;
using MrGutter.Web.Models;
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
            if(estimateVM.FirstName == null || estimateVM.Addressline1 == null)
            {
                TempData["Flag"] = "E";
                bool estimateCreationFailed = true;  
                TempData["EstimateError"] = estimateCreationFailed ? "True" : "False";
                return RedirectToAction("EstimateList");
            }
            estimateVM.CreatedBy = HttpContext.Session.GetInt32("UserId") ?? 0;
            estimateVM.UserID = estimateVM.CreatedBy;
            var result = await _estimatesService.CreateEstimateAsync(estimateVM);
            return RedirectToAction("EstimateList");
        }

        public async Task<IActionResult> EstimateList(int estimatorId, string Search, int StatusID, int CreationStatus)
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsModel estimateIdsModel = new EstimateIdsModel();
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM
            {
                CompanyID = companyId,
                UserId = UserId
            };
            EstimateVM estimateVM = new EstimateVM();
            List<EstimateVM> filteredEstimates = new List<EstimateVM>();
            if (estimatorId == 0)
            {
                ViewBag.filteredEstimatorIdMsg = false;
                estimateVM.UserID = UserId;
            }
            else
            {
                ViewBag.filteredEstimateIdMsg = true;
                estimateVM.UserID = estimatorId;
            }

            if (StatusID == 0)
            {
                ViewBag.filteredStatusIdMsg = false;
            }
            else
            {
                ViewBag.filteredStatusMsg = true;
            }
            try
            {
                var statusColor = await _estimatesService.GetStatuslistAsync("");
                estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
                filteredEstimates = estimateVM.EstimateList;

                if (estimatorId != 0 && StatusID != 0 && CreationStatus == 0) {

                    filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                    filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();
                }
                if (estimatorId != 0 && CreationStatus != 0 && StatusID == 0)
                {

                    var creationStatusFilter = new CreationStatusFilter();
                    DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                    filteredEstimates = filteredEstimates.Where(x =>
                        DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                        estimateDate >= startDate).ToList();
                    filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                }
                if (StatusID != 0 && estimatorId != 0 && CreationStatus == 0)
                {
                    filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();

                    filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                }
                if (StatusID != 0 && CreationStatus != 0 && estimatorId == 0)
                {

                    var creationStatusFilter = new CreationStatusFilter();
                    DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                    filteredEstimates = filteredEstimates.Where(x =>
                        DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                        estimateDate >= startDate).ToList();
                    filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();
                }
                if (CreationStatus != 0 && estimatorId != 0 && StatusID == 0)
                {
                    var creationStatusFilter = new CreationStatusFilter();
                    DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                    filteredEstimates = filteredEstimates.Where(x =>
                        DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                        estimateDate >= startDate).ToList();

                    filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                }
                if (CreationStatus != 0 && StatusID != 0 && estimatorId == 0)
                {
                    var creationStatusFilter = new CreationStatusFilter();
                    DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                    filteredEstimates = filteredEstimates.Where(x =>
                        DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                        estimateDate >= startDate).ToList();

                    filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();
                }
                if (CreationStatus != 0 && StatusID != 0 && estimatorId != 0)
                {
                    var creationStatusFilter = new CreationStatusFilter();
                    DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                    filteredEstimates = filteredEstimates.Where(x =>
                        DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                        estimateDate >= startDate).ToList();
                    filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                    filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();
                }
                else
                {
                    if (estimatorId != 0 && StatusID == 0 && CreationStatus ==0)
                    {
                        filteredEstimates = filteredEstimates.Where(x => x.UserID == estimatorId).ToList();
                    }

                    if (StatusID > 0 && CreationStatus == 0 && estimatorId == 0) 
                    {
                        filteredEstimates = filteredEstimates.Where(x => x.StatusID == StatusID).ToList();
                    }
                    if (CreationStatus > 0 && StatusID == 0 && estimatorId == 0)
                    {
                        var creationStatusFilter = new CreationStatusFilter();
                        DateTime startDate = creationStatusFilter.GetStartDate(CreationStatus);
                        filteredEstimates = filteredEstimates.Where(x =>
                            DateTime.TryParse(x.EstimateCreatedDate, out DateTime estimateDate) &&
                            estimateDate >= startDate).ToList();
                    }
                }
                if (estimatorId !=0)
                {
                    estimateVM.UserID = estimatorId;
                }
                else
                {
                    estimateVM.UserID = UserId;
                }
                var allRole = await _userManagerService.GetRoleAsync("");
                var allUser = await _userManagerService.GetUserAsync("");
                var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
                var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();
                estimateVM.EstimatorUsers = estimatorUsers;
                estimateVM.StatusList = statusColor.StatusList;
                estimateVM.EstimateList = filteredEstimates;
            }
            catch (Exception ex)
            {
               
            }
            return View(estimateVM);
        }

        [HttpPost]
        public async Task<IActionResult> EstimateData(int estimatorId)
        {
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM
            {
                CompanyID = companyId,
                UserId = userId
            };

            if (estimatorId == 0)
            {
                estimatorId = userId;
            }

            var result = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

           var originalEstimateList = result.EstimateList.Where(x => x.UserID == estimatorId).ToList();
            var estimateList = result.EstimateList
                            .Where(x => x.UserID == estimatorId)
                            .AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                estimateList = estimateList
                    .Where(x => x.EstimateNo.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
                                (x.FirstName + " " + x.LastName).Contains(searchValue, StringComparison.OrdinalIgnoreCase)||
                                (x.Addressline1 + " " + x.Addressline2 + " " + x.City + " " + x.State + " " + x.ZipCode).Contains(searchValue, StringComparison.OrdinalIgnoreCase));
            }

            var AllStatus = await _estimatesService.GetStatuslistAsync("");
            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();

            int recordsTotal = estimateList.Count();
            var data = estimateList
                .Select(estimate => new
                {
                    id = estimate.EstimateID,
                    est_no = estimate.EstimateNo,
                    est_name = estimate.FirstName + " " + estimate.LastName,
                    address = estimate.Addressline1,
                    salesperson = estimate.FirstName,
                    created = GetTimeAgo(estimate.EstimateCreatedDate),
                    status = AllStatus.StatusList
                        .Where(x => x.StatusID == estimate.StatusID.ToString())
                        .Select(x => x.StatusName)
                        .FirstOrDefault()
                }).ToList();

            var jsonData = new
            {
                draw = draw,
                recordsFiltered = recordsTotal,
                recordsTotal = originalEstimateList.Count(),
                data = data
            };

            return Ok(jsonData);
        }
        //[HttpPost]
        //public async Task<IActionResult> EstimateData(int estimatorId)
        //{
        //    int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
        //    int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
        //    EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
        //    estimateIdsVM.CompanyID = companyId;
        //    estimateIdsVM.UserId = userId;
        //    if (estimatorId == 0)
        //    {
        //        estimatorId = userId;
        //    }
        //    var result = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
        //    var draw = Request.Form["draw"].FirstOrDefault();
        //    var start = Request.Form["start"].FirstOrDefault();
        //    var length = Request.Form["length"].FirstOrDefault();
        //    var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //    var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //    var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //    int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //    int skip = start != null ? Convert.ToInt32(start) : 0;
        //    var originalEstimateList = result.EstimateList.Where(x => x.UserID == estimatorId).ToList();
        //    var estimateList = originalEstimateList.AsQueryable();
        //    var AllStatus = await _estimatesService.GetStatuslistAsync("");
        //    var allRole = await _userManagerService.GetRoleAsync("");
        //    var allUser = await _userManagerService.GetUserAsync("");
        //    var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
        //    var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();

        //    // Sorting
        //    //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //    //{
        //    //    bool descendingOrder = sortColumnDirection == "asc" ? false : true;
        //    //    estimateList = estimateList.OrderByProperty(sortColumn, descendingOrder);
        //    //}

        //    // Searching
        //    //if (!string.IsNullOrEmpty(searchValue))
        //    //{
        //    //    estimateList = estimateList.Where(m =>
        //    //        (m.FirstName + " " + m.LastName).Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //    //        m.Email.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //    //        m.EstimateNo.Contains(searchValue, StringComparison.OrdinalIgnoreCase) ||
        //    //        m.Addressline1.Contains(searchValue, StringComparison.OrdinalIgnoreCase) 

        //    //}

        //    int recordsTotal = estimateList.Count();
        //    var data = estimateList
        //                    .Select(estimate => new {
        //                        id = estimate.EstimateID,
        //                        est_no = estimate.EstimateNo,
        //                        est_name = estimate.FirstName+" "+ estimate.LastName,
        //                        address = estimate.Addressline1,
        //                        salesperson = estimate.FirstName,
        //                        created = estimate.EstimateCreatedDate,
        //                        status = AllStatus.StatusList.Where (x=>x.StatusID == estimate.StatusID.ToString()).Select(x=>x.StatusName).FirstOrDefault()
        //                    }).ToList();
        //    var jsonData = new { draw   = draw, recordsFiltered = recordsTotal, recordsTotal = originalEstimateList.Count(), data = data };
        //    return Ok(jsonData);
        //}

        private string GetTimeAgo(string estimateCreatedDateString)
        {
            DateTime estimateCreatedDate;
            if (!DateTime.TryParse(estimateCreatedDateString, out estimateCreatedDate))
            {
                return "Invalid Date";
            }

            var timeDifference = DateTime.Now - estimateCreatedDate;

            if (timeDifference.TotalDays < 1)
            {
                if (timeDifference.TotalHours < 1)
                {
                    return $"{timeDifference.Minutes} minute{(timeDifference.Minutes > 1 ? "s" : "")} ago";
                }
                else
                {
                    return $"{timeDifference.Hours} hour{(timeDifference.Hours > 1 ? "s" : "")} ago";
                }
            }
            else if (timeDifference.TotalDays <= 7)
            {
                return $"{(int)timeDifference.TotalDays} day{(timeDifference.TotalDays > 1 ? "s" : "")} ago";
            }
            else if (timeDifference.TotalDays <= 30)
            {
                return $"{(int)timeDifference.TotalDays} day{(timeDifference.TotalDays > 1 ? "s" : "")} ago";
            }
            else
            {
                return "older than 30 days";
            }
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
            //int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            //estimateIdsVM.UserId = userId;
            estimateIdsVM.EstimateID = id;
            estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);
            var StatusColor = await _estimatesService.GetStatuslistAsync("");
            estimateVM.StatusList = StatusColor.StatusList;
            return View(estimateVM);
        }
    

        public async Task<IActionResult> EstimationSettings(int EstimateId)
        {
            EstimateVM estimateVM = new EstimateVM();
            int companyId = HttpContext.Session.GetInt32("CompanyId") ?? 0;
            EstimateIdsVM estimateIdsVM = new EstimateIdsVM();
            estimateIdsVM.CompanyID = companyId;
            estimateIdsVM.EstimateID = EstimateId;
            var StatusColor = await _estimatesService.GetStatuslistAsync("");

            estimateVM = await _estimatesService.GetEstimatelistAsync(estimateIdsVM);

            var allRole = await _userManagerService.GetRoleAsync("");
            var allUser = await _userManagerService.GetUserAsync("");
            var estimatorRole = allRole.Roles.FirstOrDefault(r => r.RoleName == "Estimator");
            var estimatorUsers = allUser.Users.Where(u => u.UserType == "Estimator").ToList();
            var catList = await _estimatesService.GetMeasurementCatListAsync(0, companyId);
            var tokenList = await _estimatesService.GetMeasurementTokenListAsync(EstimateId, 0, 0);
            var unitList = await _estimatesService.GetMeasurementUnitListAsync(0, companyId);
            estimateVM.MeasurementCat = catList.MeasurementCat;
            estimateVM.MeasurementToken = tokenList.MeasurementToken;
            estimateVM.MeasurementUnit = unitList.MeasurementUnit;
            estimateVM.EstimatorUsers = estimatorUsers;
            estimateVM.EstimateID = estimateVM.EstimateList[0].EstimateID ;
            return View(estimateVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEstimate(int estimateId)
        {
            EstimateVM estimateVM = new EstimateVM();
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            estimateVM.EstimateID = estimateId;
            estimateVM.UserID = userId;
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
        [HttpPost]
        public async Task<IActionResult> PropertyMeasurement(int MTokenID , int estimateID , string TokenValue)
        {

            var result = await _estimatesService.UpdateMeasurementTokenValueAsync(MTokenID, estimateID, TokenValue);
            return RedirectToAction("EstimateList");
        }

    }
}
