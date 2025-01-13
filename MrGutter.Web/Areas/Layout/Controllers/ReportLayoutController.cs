using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrGutter.Models.ViewModels;
using MrGutter.Services.IService;
using System.Net;

namespace MrGutter.Web.Areas.Layout.Controllers
{
    [Area("Layout")]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class ReportLayoutController : Controller
    {
        private readonly ILayoutManagerService _layoutManagerService;
        public ReportLayoutController(ILayoutManagerService layoutManagerService)
        {
            _layoutManagerService = layoutManagerService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLayout(string? layoutName, Boolean? isShare)
        {
            CreateReportLayoutVM createReportVM = new CreateReportLayoutVM();
            createReportVM.LayoutName = layoutName;
            createReportVM.IsShared = isShare;
            if (!ModelState.IsValid)
            {
                return View("ReportLayoutList");
            }
            var response = await _layoutManagerService.CreateLayoutAsync(createReportVM);
            return View();
        }


        public IActionResult ReportLayoutList()
        {
            return View();
        }
        public IActionResult EditReportLayout()
        {
            return View();
        }
        public IActionResult EditLayoutIntroduction()
        {
            return View();
        }
        public IActionResult EditLayoutInspection()
        {
            return View();
        }
        
        public IActionResult EditMapLayout()
        {
            return View();
        }
        public IActionResult EditLayoutTitle()
        {
            return View();
        }
        public IActionResult EditQuoteDetails()
        {
            return View();
        }
        public IActionResult AuthorizationPage()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult LayoutAuthorizationPage()
        {
            return View();
        }
        public IActionResult LayoutTermConditionPage()
        {
            return View();
        }
        public IActionResult LayoutWarrantyPage()
        {
            return View();
        }
        public IActionResult LayoutCustomPage()
        {
            return View();
        }



        public IActionResult Test1()
        {
            return View();
        }
        public IActionResult Test2()
        {
            return View();
        }
        public IActionResult Test3()
        {
            return View();
        }
    }
}
