using MrGutter.Models;
using MrGutter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<APIResponseModel> GetRoleAsync(string? roleId);
        public Task<APIResponseModel> GetRoleByUserIdAsync(int userId);
        public Task<APIResponseModel> GetGroupAsync(string? groupId);
        public Task<APIResponseModel> GetUserAsync(string? userId);
        public Task<APIResponseModel> CreateOrUpdateUser(User userInfo);
        public Task<APIResponseModel> DeleteUser(User userInfo);
        public Task<APIResponseModel> GetCompanyAsync(string? userId);
        public Task<APIResponseModel> CreateOrUpdateCompany(Company cmpInfo);
        public Task<APIResponseModel> DeleteCompanyAsync(Company cmpInfo);


    }
}
