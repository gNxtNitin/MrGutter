using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Report.Controllers
{
    [Area("Report")]
    public class ReportController : Controller
    {
        public IActionResult CreateReport()
        {
            return View();
        }

        public IActionResult Introduction()
        {
            return View();
        }

        public IActionResult Inspection()
        {
            return View();
        }

        public IActionResult ReportSettings()
        {
            return View();
        }
        public IActionResult QuoteDetails()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }
        public IActionResult Warranty()
        {
            return View();
        }
        public IActionResult Authorize()
        {
            return View();
        }
        public IActionResult AddCustomPage()
        {
            return View();
        }
        public IActionResult CreateMap()
        {
            return View();
        }
        public IActionResult ReviewAndShare()
        {
            return View();
        }
    }
}
