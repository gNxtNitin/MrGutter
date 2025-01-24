using MrGutter.Models;
using MrGutter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services.IService
{
    public interface IUserManagerService
    {
        public Task<RoleVM> GetRoleByUserIdAsync(int userId);

        //For multiple roles
        public Task<List<UserRoleModel>> GetUserRole(int userId);
        public Task<List<UserCompanyModel>> GetUserCompany(int userId);
        public Task<UsersVM> GetUserAsync(string? UserId);
        public Task<RoleVM> GetRoleAsync(string? roleId);
        public Task<int> CreateOrUpdateUser(UsersVM user);
        public Task<int> DeleteUser(UsersVM user);
        public Task<CompanyVM> GetCompanyAsync(string? cId);
        public Task<int> CreateOrUpdateCompany(CompanyVM cmpInfo);
        public Task<int> DeleteCompanyAsync(CompanyVM cmpInfo);



        //public Task<int> UpdateUser(UsersVM user);
    }
}
