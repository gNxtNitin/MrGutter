using MrGutter.Models;
using MrGutter.Models.ViewModels;
using MrGutter.Repository.IRepository;
using MrGutter.Services.IService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly IUserRepository _userRepository;
        public UserManagerService(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }
        public async Task<RoleVM> GetRoleByUserIdAsync(int userId)
        {
            RoleVM roles = new RoleVM();
            var response = await _userRepository.GetRoleByUserIdAsync(userId);
            if(response.Data != null)
            {
                roles = JsonConvert.DeserializeObject<RoleVM>(response.Data);
            }
            return roles;
        }
        public async Task<UsersVM> GetUserAsync(int UserId)
        {
            UsersVM usersVM = new UsersVM();
            var response = await _userRepository.GetUserAsync(UserId);
            usersVM = JsonConvert.DeserializeObject<UsersVM>(response.Data);
            return usersVM;
        }
    }
}
