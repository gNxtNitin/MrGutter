using MrGutter.Models;
using MrGutter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services.IService
{
    public interface IAccountService
    {
        public Task<APIResponseModel> AuthenticateUser(LoginVM loginModel);
        public Task<int> SendForgotPasswordEmailAsync(LoginVM loginModel);
        public Task<int> CreateAaccountAsync(UsersVM usersVM);
        public Task<int> SetOtpAsync(UsersVM usersVM);
        public Task<int> ValidateOTPAsync(LoginVM loginModel);
        public Task<UsersVM> GetUserAsync(string? UserId);
        public Task<int> ResetPasswordAsync(LoginVM loginVM);
    }
}
