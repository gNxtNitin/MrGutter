using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrGutter.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address")]
        public string? EmailOrMobile { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and include at least one letter, one number, and one special character.")]
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        public Boolean RememberMe { get; set; }
        public Boolean IsLoginWithOtp { get; set; } = false;
        public string? VerificationCode { get; set; }
        public Boolean IsResendOTP { get; set; } = false;
    }
}
