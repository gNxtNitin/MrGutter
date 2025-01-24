using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MrGutter.Models;
using MrGutter.Services.IService;
using System.Threading.Tasks;
using System.Linq;
using MrGutter.Models.ViewModels;

namespace MrGutter.Web.ViewComponents
{
    public class UserHeaderInfoViewComponent : ViewComponent
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserManagerService _userManagerService;

        public UserHeaderInfoViewComponent(IUserManagerService userManagerService, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _userManagerService = userManagerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            var CompanyId = _httpContextAccessor.HttpContext?.Session.GetInt32("CompanyId");
            if (userId == null)
            {
                return View(new UsersVM());
            }
            var userDetails = await _accountService.GetUserAsync(userId.ToString());
            var companyDetails = await _userManagerService.GetCompanyAsync(CompanyId.ToString());
            var cmp = companyDetails.Company.FirstOrDefault();
            var user = userDetails.Users.FirstOrDefault();
            List<UserCompanyModel> c = new List<UserCompanyModel>();

            UserCompanyModel userCompany = new UserCompanyModel
            {
                CompanyId = cmp.CompanyId
            };
            c.Add(userCompany);
            var viewModel = new User
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                CompanyList = c
            };
            return View(viewModel);
        }
    }
}
