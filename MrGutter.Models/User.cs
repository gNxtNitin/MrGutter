using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models
{
    public class User
    {
        public string UserID { get; set; } = "0";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public string DOB { get; set; }
        public string CreatedBy { get; set; } = "0";
        public string LastLoginDate { get; set; }
        public string UserStatus { get; set; }
        public string Initials
        {
            get
            {
                char? firstInitial = string.IsNullOrEmpty(FirstName) ? (char?)null : FirstName[0];
                char? lastInitial = string.IsNullOrEmpty(LastName) ? (char?)null : LastName[0];

                if (firstInitial.HasValue && lastInitial.HasValue)
                {
                    return $"{firstInitial}{lastInitial}".ToUpper();
                }
                else if (firstInitial.HasValue)
                {
                    return $"{firstInitial}".ToUpper();
                }
                else if (lastInitial.HasValue)
                {
                    return $"{lastInitial}".ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }


}
