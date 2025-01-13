using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class UsersVM
    {
        public string UserID { get; set; } = "0";
        public string CompanyId { get; set; } = "0";
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string PointOfContact { get; set; }

        public string RoleID { get; set; } = "1";
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(10, ErrorMessage = "Phone Number cannot exceed 10 digits")]
        public string? Mobile { get; set; }

        public string? UserName { get; set; }
        public DateTime? DOB { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[!@#$%^&*(),.?""{}|<>]).+$", ErrorMessage = "Password must contain at least one special character.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string? UserType { get; set; }
        public string? UserStatus { get; set; }
        public string? SelectQues { get; set; }
        public bool isActive { get; set; }
        public string? Flag { get; set; }
        public List<User>? Users { get; set; }
    }
  
}