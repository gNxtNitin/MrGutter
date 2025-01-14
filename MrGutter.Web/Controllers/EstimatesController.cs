using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Services.IService;

namespace MrGutter.Web.Controllers
{
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class EstimatesController : Controller
    {
        private readonly IEstimatesService _estimatesService;
        private readonly IAccountService _accountService;
        public  EstimatesController(IEstimatesService estimatesService, IAccountService accountService)
        {
            _estimatesService = estimatesService;
            _accountService = accountService;
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
             estimateVM =await _estimatesService.GetEstimatelistAsync("");
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

        public IActionResult EstimationDetails(EstimateVM estimateVM)
        {
            
            return View(estimateVM);
        }
        public async Task<IActionResult> EstimationSettings()
        {
            var result = await _accountService.GetRoleAsync("");
            EstimateVM estimateVM = new EstimateVM();
           
            return View();
        }
    }
}
