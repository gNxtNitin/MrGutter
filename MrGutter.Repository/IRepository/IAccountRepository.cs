using MrGutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<APIResponseModel> AuthenticateUser(LoginModel loginReqModel);
        public Task<APIResponseModel> SetOTPAsync(LoginModel loginReqModel);
        public Task<APIResponseModel> ValidateOTPAsync(LoginModel req);
        public Task<APIResponseModel> ResetPasswordAsync(LoginModel req);
        public Task<APIResponseModel> SendForgotPasswordEmailAsync(LoginModel loginReqModel);
        public Task<APIResponseModel> CreateAaccountAsync(User user);

        public Task<APIResponseModel> SetOtpAsync(User user);
        public Task<APIResponseModel> GetUserAsync(string? UserId);
        public Task<APIResponseModel> GetRoleAsync(string? RoleId);
        //   public Task<APIResponseModel> SendForgotPasswordEmailAsync(LoginModel loginModel);

    }
}
