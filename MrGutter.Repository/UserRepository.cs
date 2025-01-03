using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MrGutter.Repository
{
    public class UserRepository : IUserRepository
    {
        APIWrapper _aPIWrapper = new APIWrapper();
        EncryptDecrypt _encryptDecrypt = new EncryptDecrypt();
        public async Task<APIResponseModel> GetGroupAsync(string? groupId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                //string json = JsonConvert.SerializeObject(loginReqModel);
                //string V = await encryptDecrypt.Encrypt(json);
                //string reqStr = HttpUtility.UrlEncode(json);

                //Call the API
                response = await _aPIWrapper.GetAsync("UserManager/GetGroups", "", "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }
        public async Task<APIResponseModel> GetRoleAsync(int roleId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string V = await _encryptDecrypt.Encrypt(roleId.ToString());
                string reqStr = HttpUtility.UrlEncode(V);
                response = await _aPIWrapper.GetAsync("UserManager/GetRoles?encReq=", reqStr, "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }

        public async Task<APIResponseModel> GetRoleByUserIdAsync(int userId)
        {
            APIResponseModel response = new APIResponseModel();
            try
            {
                string V = await _encryptDecrypt.Encrypt(userId.ToString());
                string reqStr = HttpUtility.UrlEncode(V);
                response = await _aPIWrapper.GetAsync("UserManager/GetRoleByUserId?encReq=", reqStr, "");
            }
            catch (Exception ex)
            {
                response = new APIResponseModel();
                response.Code = -1;
                response.Msg = ex.Message;
            }
            return response;
        }

        public async Task<APIResponseModel> GetUserAsync(int userId)
        {
            APIResponseModel apiResponse = new APIResponseModel();
            try
            {
                string json = JsonConvert.SerializeObject(userId);
                //string V = await _encryptDecrypt.Encrypt(json);
                //string reqStr = HttpUtility.UrlEncode(V);

                //Call the API
                apiResponse = await _aPIWrapper.GetAsync("api/UserManager/GetUsers?encReq=", json, "");
            }
            catch (Exception ex)
            {
                apiResponse = new APIResponseModel();
                apiResponse.Code = -1; 
                apiResponse.Msg = ex.Message;
            }
            return apiResponse;
        }
    }
}
