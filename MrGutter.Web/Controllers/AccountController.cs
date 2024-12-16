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
namespace MrGutter.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserManagerService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        EncryptDecrypt _encryptDecrypt = new EncryptDecrypt();
        public AccountController(IAccountService accountService, IUserManagerService userService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult User()
        {
            return View();
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult RegisterCompany()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginReq)
        {
            if (!ModelState.IsValid)
            {
                return View(loginReq);
            }
            try
            {
                var result = await _accountService.AuthenticateUser(loginReq);
               // result.Code = 1;
                HttpContext.Session.SetInt32("UserId", Convert.ToInt32(result.Code));
                if (result.Code <= 0)
                {
                    ViewBag.LoginErrorMsg = "Invalid Credentials.";
                    return View(loginReq);
                }
                 var roleManager = await _userService.GetRoleByUserIdAsync(Convert.ToInt32(result.Code));
                 var currentUserRole = roleManager.Roles.FirstOrDefault();
                if (currentUserRole == null)
                {
                    ViewBag.LoginErrorMsg = "Invalid Credentials.";
                    return View(loginReq);
                }
                var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name, user.Users[0].UserName),
                //new Claim(ClaimTypes.Email, user.Users[0].UserName),
                new Claim(ClaimTypes.Role, currentUserRole.RoleName),
                new Claim(ClaimTypes.NameIdentifier, result.Code.ToString())
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return RedirectToAction("Dashboard", "Dashboard", new { area = "" });
            }
            catch (Exception ex)
            {
                ViewBag.LoginErrorMsg = "An error occurred.";
                return View(loginReq);
            }
        }
        public async Task<IActionResult> LogOut(LoginVM loginReqModel)
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
        public IActionResult ChangePassword()
        {
            TempData["Success"] = true;
            TempData["SuccessMessage"] = "We've just sent you an email to reset your password.";
           
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
                return View(model);
            }
            else
            {
                ViewBag.errormsg = "User doesn't exist";
                return View(model);
            }
        }
        //public IActionResult ChangePassword(string email)
        //{
        //    TempData["Success"] = true;
        //    TempData["SuccessMessage"] = "Ticket category added successfully!";
        //    UsersVM usersVM = new UsersVM();    
        //    usersVM.EmailID = email;    
        //    return View(usersVM);
        //}
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UsersVM usersVM)
        {
            if(usersVM.Password == null)
            {
                ViewBag.errormsg = "Password is required";
                return View(usersVM);
            }
            else if (usersVM.ConfirmPassword == null)
            {
                ViewBag.error = "Confirm password is required";
                return View(usersVM);
            }
            var result = await _accountService.ResetPasswordAsync(usersVM);
            if (result > 0)
            {
                return RedirectToAction("SuccessChangePassword");  
            }
            return View();
        }

        public IActionResult SuccessChangePassword()
        {
            return View();
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
         
            //    if (details.data != null)
            //    {
            //        NewCustomerOtpVerificationReqModel obj = new NewCustomerOtpVerificationReqModel();
            //        obj.UserId = details.code;
            //        obj.CaseId = Convert.ToInt32(details.data);
            //        obj.MobileNo = requestmodel.MobileNo;
            //        obj.Email = requestmodel.Email;

            //        string json = JsonConvert.SerializeObject(obj);
            //        string encReq = await encryptDecrypt.Encrypt(json);
            //        encReq = System.Net.WebUtility.UrlEncode(encReq);
            //        return RedirectToAction("OtpVerification", "Home", new { enc = encReq });
            //    }
            //    else
            //    {
            //        return View(requestmodel);
            //    }
            //}
            //else
            //{
            //    return View(requestmodel);
            //}
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
    }
}
