using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Repository.IRepository;
using MrGutter.Services.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserRepository _userRepository;
        public UserManagerService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Role
        public async Task<RoleVM> GetRoleByUserIdAsync(int userId)
        {
            RoleVM roles = new RoleVM();
            var response = await _userRepository.GetRoleByUserIdAsync(userId);
            if (response.Data != null)
            {
                roles = JsonConvert.DeserializeObject<RoleVM>(response.Data);
            }
            return roles;
        }  

        public async Task<RoleVM> GetRoleAsync(string? roleId)
        {
            RoleVM roles = new RoleVM();
            var response = await _userRepository.GetRoleAsync(roleId);
            if (response.Data != null)
            {
                roles = JsonConvert.DeserializeObject<RoleVM>(response.Data);
            }
            return roles;
        }
        #endregion

        #region User
        public async Task<UsersVM> GetUserAsync(string? UserId)
        {
            UsersVM usersVM = new UsersVM();
            var response = await _userRepository.GetUserAsync(UserId);
            usersVM = JsonConvert.DeserializeObject<UsersVM>(response.Data);
            return usersVM;
        }
        public async Task<int> CreateOrUpdateUser(UsersVM user)
        {
            int result = 0;

            
            User obj = new User();
            //obj.UserName = user.UserName;
            obj.FirstName = user.FirstName;
            obj.isActive = user.isActive;
            obj.LastName = user.LastName;
            obj.UserID  = user.UserID;
            obj.RoleID = user.RoleID;
            obj.CompanyId = user.CompanyId;
            obj.CreatedBy = user.CreatedBy;
            //obj.UserName = user.UserName;
            obj.Email = user.Email;
            obj.Mobile = user.Mobile;
            obj.UserStatus = user.UserStatus;
            obj.UserType= user.UserType;
            if (user.UserID == "0")
            {
                obj.Flag = "C";
            }
            else
            {
                obj.Flag = "U";
            }
            var res = await _userRepository.CreateOrUpdateUser(obj);
            if (res.Code >= 0)
            {
                result = res.Code;
                return result;
            }
            return result;
        }
        public async Task<int> DeleteUser(UsersVM user)
        {
            int result = 0;
            User obj = new User();
            //obj.isActive = user.isActive;
            obj.Flag = "D";
            obj.UserID = user.UserID;
            var res = await _userRepository.CreateOrUpdateUser(obj);
            if (res.Code >= 0)
            {
                result = res.Code;
                return result;
            }
            return result;
        }
        #endregion

        #region Company
        public async Task<CompanyVM> GetCompanyAsync(string? cId)
        {
            CompanyVM CompVM = new CompanyVM();
           
            var response = await _userRepository.GetCompanyAsync(cId);
            CompVM = JsonConvert.DeserializeObject<CompanyVM>(response.Data); 
            return CompVM;
        }
        public async Task<int> CreateOrUpdateCompany(CompanyVM cmpInfo)
        {
            int result = 0;
            Company obj = new Company();
            obj.CompanyName = cmpInfo.CompanyName;
            obj.CompanyPhone = cmpInfo.CompanyPhone;
            obj.CompanyEmail = cmpInfo.CompanyEmail;
            obj.ContactPerson = cmpInfo.ContactPerson;
            obj.CompanyId = cmpInfo.CompanyId;


            if (cmpInfo.CompanyId == "0")
            {
                obj.Flag = "C";
            }
            else
            {
                obj.Flag = "U";
            }
            var res = await _userRepository.CreateOrUpdateCompany(obj);
            if (res.Code >= 0)
            {
                result = res.Code;
                return result;
            }
            return result;
        }
        public async Task<int> DeleteCompanyAsync(CompanyVM cmpInfo)
        {
            int result = 0;
            Company obj = new Company();
           // obj.isActive = cmpInfo.isActive;
            obj.Flag = "D";
            obj.CompanyId = cmpInfo.CompanyId;
            var res = await _userRepository.CreateOrUpdateCompany(obj);
            if (res.Code >= 0)
            {
                result = res.Code;
                return result;
            }
            return result;
        }

        #endregion
    }
}
