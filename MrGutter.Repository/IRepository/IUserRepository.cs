using MrGutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Repository.IRepository
{
    public interface IUserRepository
    {
        public Task<APIResponseModel> GetRoleAsync(int roleId);
        public Task<APIResponseModel> GetRoleByUserIdAsync(int userId);
        public Task<APIResponseModel> GetGroupAsync(string? groupId);
        public Task<APIResponseModel> GetUserAsync(string? userId);
    }
}
