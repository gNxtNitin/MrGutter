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

        public UserHeaderInfoViewComponent(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            if (userId == null)
            {
                return View(new UsersVM());
            }
              var userDetails = await _accountService.GetUserAsync(userId.ToString());
            var user = userDetails.Users.FirstOrDefault();

            var viewModel = new User
            {
                FirstName = user?.FirstName,
                LastName = user?.LastName
            };
            return View(viewModel);
        }
    }
}
