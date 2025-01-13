using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Repository.IRepository;
using MrGutter.Services.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        public AccountService(IAccountRepository accountRepository)
        {
                _accountRepo = accountRepository;
        }
        public async Task<APIResponseModel> AuthenticateUser(LoginVM loginModel)
        {
            LoginModel reqModel=new LoginModel();
            reqModel.MobileOrEmail = loginModel.EmailOrMobile;
            reqModel.Password = loginModel.Password;

            return await _accountRepo.AuthenticateUser(reqModel);
        }

        public async Task<int> CreateAaccountAsync(UsersVM usersVM)
        {
         int result = 0 ;
         User model = new User();
            model.FirstName = usersVM.FirstName;    
            model.LastName = usersVM.LastName;
            model.Email = usersVM.Email;
            model.Mobile = usersVM.Mobile;
            model.DOB = usersVM.DOB;
            model.Password = usersVM.ConfirmPassword;
           var  response = await _accountRepo.CreateAaccountAsync(model);
            if(response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<int> ResetPasswordAsync(LoginVM loginVM)
        {
            int result = 0;
            LoginModel model = new LoginModel();
            model.MobileOrEmail = loginVM.EmailOrMobile;
            model.Password = loginVM.ConfirmPassword;
            var response = await _accountRepo.ResetPasswordAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<int> SetOtpAsync(UsersVM usersVM)
        {
            int result = 0;
            User model = new User();
            model.Email = usersVM.Email;
            var response = await _accountRepo.SetOtpAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<int> ValidateOTPAsync(LoginVM login)
        {
            int result = 0;
            LoginModel model = new LoginModel();
            model.MobileOrEmail = login.EmailOrMobile;
            model.VerificationCode = login.VerificationCode;
            var response = await _accountRepo.ValidateOTPAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
        public async Task<UsersVM> GetUserAsync(string? UserId)
        {
            UsersVM usersVM = new UsersVM();
            var response = await _accountRepo.GetUserAsync(UserId);
            usersVM = JsonConvert.DeserializeObject<UsersVM>(response.Data);
            return usersVM;
        }
        public async Task<int> SendForgotPasswordEmailAsync(LoginVM loginModel)
        {
           int result = 0;
           LoginModel model = new LoginModel();
           model.MobileOrEmail = loginModel.EmailOrMobile;
           var response = await _accountRepo.SendForgotPasswordEmailAsync(model);
            if (response.Code > 0)
            {
                result = response.Code;
            }
            return result;
        }
    }
}
