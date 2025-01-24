using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models
{
    public class RoleModel 
    {
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = false;

    }
    public class UserRoleModel
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public int UserId { get; set; } = 0;
        public bool? IsActive { get; set; } = false;
    }
    public class UserCompanyModel
    {
        public int UserId { get; set; } = 0;
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public bool? IsActive { get; set; } = false;
    }
}
