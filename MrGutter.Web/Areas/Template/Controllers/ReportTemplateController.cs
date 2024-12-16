using Microsoft.AspNetCore.Mvc;

namespace MrGutter.Web.Areas.Template.Controllers
{
    [Area("Template")]
    public class ReportTemplateController : Controller
    {
       
        public IActionResult ReportTemplateList()
        {
            return View();
        }
        public IActionResult IntroductionTemplate(string? introTempID)
        {
            return View();
        }
        public IActionResult QuoteDetailTemplate(string? quoteTempID)
        {
            return View();
        }
        public IActionResult AuthorizationTemplate(string? authTempID)
        {
            return View();
        }
        public IActionResult CustomPageTextTemplate(string? cusTextTempID)
        {
            return View();
        }
        public IActionResult EmailTemplate()
        {
            return View();
        }
        public IActionResult InvoiceEmailTemplate()
        {
            return View();
        }
       
    }
}
