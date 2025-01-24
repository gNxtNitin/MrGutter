using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using MrGutter.Models.ViewModels;
using MrGutter.Services.IService;
using MrGutter.Repository;
using Microsoft.AspNetCore.Identity;
using MrGutter.Models;
using MrGutter.Web.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace MrGutter.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserManagerService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountController> _logger;

        EncryptDecrypt _encryptDecrypt = new EncryptDecrypt();
        public AccountController(IAccountService accountService, IUserManagerService userService, IHttpContextAccessor httpContextAccessor, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

     
        public IActionResult RegisterCompany()
        {
            return View();
        }
        public IActionResult EditPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(ResetPasswordVM resetPassword)
        {
            LoginVM loginVM = new LoginVM();
            var userId = HttpContext.Session.GetInt32("UserId");
            var user = await _userService.GetUserAsync(userId.ToString());
            var userEmail = user.Users.FirstOrDefault(m => m.UserID == userId.ToString());

            loginVM.EmailOrMobile = userEmail.Email;
            loginVM.Password = resetPassword.Password;
            loginVM.ConfirmPassword = resetPassword.ConfirmPassword;
            if (loginVM.Password == loginVM.ConfirmPassword)
            {
                var result = await _accountService.ResetPasswordAsync(loginVM);
                if (result > 0)
                {
                    return RedirectToAction("SuccessEditPassword");
                }
            }
            else
            {
                ViewBag.ConfirmPassword = "The password and confirmation password do not match.";
                return View(resetPassword);

            }
            return View();
        }



        public IActionResult SuccessEditPassword()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult MyProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginReq)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.LoginError = "Invalid";
                return View("Index", loginReq);
            }
            try
            {
                var result = await _accountService.AuthenticateUser(loginReq);
                var token = result.Data;
                if (result.Code <= 0)
                {
                    ViewBag.LoginError = "Invalid";
                    ViewBag.LoginErrorMsg = "Invalid Credentials.";
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", Convert.ToInt32(result.Code));
                var roleManager = await _userService.GetRoleByUserIdAsync(Convert.ToInt32(result.Code));  
                var roleName = roleManager.Roles.FirstOrDefault()?.RoleName;
                if (string.IsNullOrEmpty(roleName))
                {
                    ViewBag.LoginError = "Invalid";
                    ViewBag.LoginErrorMsg = "Invalid Credentials.";
                    return View("Index");
                }

                // Fetch company information

                var companyManager = (await _userService.GetUserCompany(result.Code)).FirstOrDefault();
                var companyId = companyManager.CompanyId;
                if (string.IsNullOrEmpty(companyId))
                {
                    ViewBag.LoginError = "Invalid";
                    ViewBag.LoginErrorMsg = "Unable to retrieve company information.";
                    return View("Index");
                }
                // Sanitize the companyId to remove non-numeric characters
                var sanitizedCompanyId = new string(companyId.Where(char.IsDigit).ToArray());
                // Parse the sanitized companyId into an integer
                if (int.TryParse(sanitizedCompanyId, out int cId))
                {
                    HttpContext.Session.SetInt32("CompanyId", cId);
                }
                else
                {
                    ViewBag.LoginError = "Invalid";
                    ViewBag.LoginErrorMsg = "Invalid company ID format.";
                    return View("Index");
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, roleName),              
                    new Claim(ClaimTypes.NameIdentifier, result.Code.ToString()), 
                    new Claim("Token", token)                 
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties
                {
                    IsPersistent = true,                   
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(15) 
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                HttpContext.Session.SetString("AuthToken", token); 
                HttpContext.Response.Cookies.Append(
                    "AuthToken",
                    token,
                    new CookieOptions
                    {
                        HttpOnly = true,    
                        Secure = true,      
                        SameSite = SameSiteMode.Strict, 
                        Expires = DateTime.UtcNow.AddMinutes(15) 
                    });
                var roleManagerdsadsa = await _userService.GetRoleByUserIdAsync(Convert.ToInt32(result.Code));
                return RedirectToAction("EstimateList", "Estimates", new { area = "" });
            }
            catch (Exception ex)
            {
                ViewBag.LoginErrorMsg = "An error occurred. Please try again.";
                return View("Index");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            var userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId");
            if (_httpContextAccessor.HttpContext?.Request.Cookies.Count > 0)
            {
                var siteCookies = _httpContextAccessor.HttpContext.Request.Cookies
                    .Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("Microsoft.Authentication"));
                foreach (var cookie in siteCookies)
                {
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookie.Key);
                }
            }
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginVM model)
        {
            if(model.EmailOrMobile == null)
            {
                ViewBag.error = "Email is required";
                return View(model);
            }
            var loginReqModel = new LoginVM
            {
                EmailOrMobile = model.EmailOrMobile,
            };
            var result = await _accountService.SendForgotPasswordEmailAsync(loginReqModel);
           if(result > 0)
            {
                ViewBag.successmsg = "We've just send you an email to reset your password.";
                return View();
            }
            else
            {
                ViewBag.errormsg = "User doesn't exist";
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordVM resetPassword)
        {
            LoginVM loginVM = new LoginVM();
            loginVM.EmailOrMobile = resetPassword.EmailID;
            loginVM.Password = resetPassword.Password;
            loginVM.ConfirmPassword = resetPassword.ConfirmPassword;
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }
            if (loginVM.Password == loginVM.ConfirmPassword)
            {
                var result = await _accountService.ResetPasswordAsync(loginVM);
                if (result > 0)
                {
                    return RedirectToAction("SuccessChangePassword");
                }
            }
            else
            {
                ViewBag.ConfirmPassword = "The password and confirmation password do not match.";
                return View(resetPassword);

            }
            return View();
        }

        public IActionResult SuccessChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> ChangePassword(string email)
        {
            ResetPasswordVM resetPasswordVM = new ResetPasswordVM();

            if (IsBase64String(email))
            {
                string decEmail = await _encryptDecrypt.Decrypt(email);
                resetPasswordVM.EmailID = decEmail;
            }
            else
            {
                // Handle invalid email format
                resetPasswordVM.EmailID = "Invalid Email Format";
            }

            return View(resetPasswordVM);
        }


        private bool IsBase64String(string str)
        {
            str = str.Trim();
            return (str.Length % 4 == 0) && Regex.IsMatch(str, @"^[a-zA-Z0-9\+/=]*$", RegexOptions.None);
        }
        public IActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAccount(UsersVM usersVM)
        {
            if (!ModelState.IsValid)
            {
                return View(usersVM);
            }
           var result = await _accountService.CreateAaccountAsync(usersVM);
            if (result > 0)
            {
                var response = await _accountService.SetOtpAsync(usersVM);
                return RedirectToAction("SendEmail");
            }
            else
            {
                ViewBag.error = "User already exists.";
                return View(usersVM);
            }
        }
        public IActionResult SendEmail()
        {
            return View();
        }
        public async Task<IActionResult> ValidateOtp(string otp , string email)
        {
            string verificationcode = await _encryptDecrypt.Decrypt(otp);
            LoginVM loginModel = new LoginVM();
            loginModel.EmailOrMobile = email;
            loginModel.VerificationCode = verificationcode;
            int userId = await _accountService.ValidateOTPAsync(loginModel);
            return RedirectToAction("EmailVerify");
        }
     
        public IActionResult EmailVerify()
        {
            return View();
        }
        public IActionResult BookDemo()
        {
            return View();
        }
        public IActionResult OnboardingCompanyDetails()
        {
            return View();
        }
        public IActionResult OnboardingBrands()
        {
            return View();
        }
        public IActionResult OnboardingTheme()
        {
            return View();
        }
        public IActionResult OnboardingConfirm()
        {
            return View();
        }
        public IActionResult OnboardingLetGoClick()
        {
            return View();
        }
        public IActionResult OnboardingBranding()
        {
            return View();
        }
        

        private long GetOtpExpirationTimeForJavaScript()
        {
            var storedExpirationTime = HttpContext.Session.GetString("OtpExpirationTime");
            if (DateTime.TryParse(storedExpirationTime, out var parsedExpirationTime))
            {
                // Convert the expiration time to Unix timestamp in milliseconds
                return (long)(parsedExpirationTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            }
            // Default expiration time (2 minutes from now) if no stored expiration time
            return (long)(DateTime.UtcNow.AddMinutes(2) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
