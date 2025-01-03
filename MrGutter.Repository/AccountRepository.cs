using Microsoft.AspNetCore.Http;
using MrGutter.Models;
using MrGutter.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MrGutter.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly APIWrapper _aPIWrapper;
        public AccountRepository(IHttpContextAccessor httpContextAccessor, APIWrapper aPIWrapper) 
        {
            _aPIWrapper = aPIWrapper;
            _httpContextAccessor = httpContextAccessor;
        }

        EncryptDecrypt _encryptDecrypt = new EncryptDecrypt();

        public async Task<APIResponseModel> AuthenticateUser(LoginModel loginReqModel)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(loginReqModel);
                //string V = await encryptDecrypt.Encrypt(json);
                string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.PostAsync("Account/AuthenticateUser", json);
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> ResetPasswordAsync(LoginModel req)
        {

            APIResponseModel APIResponseModel = new APIResponseModel();
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                APIRequestModel.MobileOrEmail = req.MobileOrEmail;
                APIRequestModel.Password = req.Password;
                string json = JsonConvert.SerializeObject(APIRequestModel);
               // APIRequestModel.V = await _encryptDecrypt.Encrypt(json);
               // string json2 = JsonConvert.SerializeObject(APIRequestModel);

                APIResponseModel = await _aPIWrapper.PostAsync("Account/ResetPassword", json);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
        public async Task<APIResponseModel> SendForgotPasswordEmailAsync(LoginModel loginReqModel)
        {
            APIResponseModel response = new APIResponseModel();
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                string json = JsonConvert.SerializeObject(loginReqModel);
                APIRequestModel.V = await _encryptDecrypt.Encrypt(json);
                string json2 = JsonConvert.SerializeObject(APIRequestModel);
                response = await _aPIWrapper.PostAsync("Account/SendForgotPasswordEmail", json2);
            }
            catch (Exception ex)
            {
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> SetOTPAsync(LoginModel loginReqModel)
        {
            APIResponseModel APIResponseModel = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(loginReqModel);
                APIResponseModel = await _aPIWrapper.PostAsync("api/Account/SetOTP", json);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
        public async Task<APIResponseModel> ValidateOTPAsync(LoginModel loginReqModel)
        {
            APIResponseModel APIResponseModel = new APIResponseModel();
            
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                string json = JsonConvert.SerializeObject(loginReqModel);
                APIRequestModel.V = await _encryptDecrypt.Encrypt(json);
                string json2 = JsonConvert.SerializeObject(APIRequestModel);
              
                APIResponseModel = await _aPIWrapper.PostAsync("Account/ValidateOTP", json2);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
        public async Task<APIResponseModel> CreateAaccountAsync(User user)
        {
            APIResponseModel APIResponseModel = new APIResponseModel();
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                string json = JsonConvert.SerializeObject(user);
                APIRequestModel.V =await _encryptDecrypt.Encrypt(json); 
                string json2 = JsonConvert.SerializeObject(APIRequestModel);

                APIResponseModel = await _aPIWrapper.PostAsync("UserManager/CreateUser", json2);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
        public async Task<APIResponseModel> SetOtpAsync(User user)
        {
            APIResponseModel APIResponseModel = new APIResponseModel();
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                string json = JsonConvert.SerializeObject(user);
                APIRequestModel.V = await _encryptDecrypt.Encrypt(json);
                string json2 = JsonConvert.SerializeObject(APIRequestModel);

                APIResponseModel = await _aPIWrapper.PostAsync("Account/SetOTP", json2);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
        public async Task<APIResponseModel> GetUserAsync(string? UserId)
        {
            APIResponseModel APIResponseModel = new APIResponseModel();
            APIRequestModel APIRequestModel = new APIRequestModel();
            try
            {
                string V = await _encryptDecrypt.Encrypt(UserId.ToString());
                string reqStr = HttpUtility.UrlEncode(V);
                APIResponseModel = await _aPIWrapper.GetAsync("UserManager/GetUsers?encReq=",reqStr);
            }
            catch (Exception ex)
            {
                APIResponseModel = new APIResponseModel();
                APIResponseModel.Code = -1;
                APIResponseModel.Msg = ex.Message;
            }
            return APIResponseModel;
        }
    }
}
