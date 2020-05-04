using Identity.Infrastructure;
using Identity.Models;
using Identity.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using AutoMapper;
using System;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private EmailService emailService;
        private IdentityResult identityRez;
        private IdentityUser user;
        private ILoggerManager log;

        public AuthenticationController(UserContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, ILoggerManager log)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.log = log;
        }

        // Registration users
        [HttpPost("registration")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> PostRegistration([FromBody] RegisterBindingRequest userData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //IdentityUser user = await userManager.FindByNameAsync("test");
                    //await userManager.DeleteAsync(user);
                    try
                    {
                        user = new IdentityUser()
                        {
                            UserName = userData.Email,
                            Email = userData.Email
                        };
                        identityRez = await userManager.CreateAsync(user, userData.Password);
                    }
                    catch (Exception ex)
                    {
                        log.LogError($"{ ex.Message } - { ex.StackTrace }");
                    }

                    if (identityRez.Succeeded)
                    {
                        //SendConfirmation(new ForgotPasswordRequest { Email = user.Email });
                        log.LogInfo($"New user {user.UserName} registration.");
                        //await roleManager.CreateAsync(new IdentityRole { Name = "Admins" });
                        await userManager.AddToRoleAsync(user, "Admins");
                        return new JsonResult(new { answer = true });
                    }
                    log.LogError(identityRez.Errors.ToString());
                    return new JsonResult(new { identityRez });
                }
            } 
            catch (Exception ex)
            {
                log.LogError($"{ ex.Message } - { ex.StackTrace }");
                return new JsonResult(new { mes = $"{ ex.Message } - { ex.StackTrace }" });
            }
            return new JsonResult(new { mes = "ErrorConf" });
        }

        //[HttpPost]
        //public async void SendConfirmation(ForgotPasswordRequest fpr)
        //{
        //    var user = await userManager.FindByEmailAsync(fpr.Email);
        //    var confirmCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var callbackUrl = Url.Action(
        //        "ConfirmEmail",
        //        "Authentication",
        //        new { userId = user.Id, code = confirmCode },
        //        protocol: Request.Scheme);
        //    emailService = new EmailService();
        //    await emailService.SendEmailAsync(user.Email, "Подтверждение электронной почты",
        //   "Для завершения регистрации перейдите по ссылке:: <a href=\""
        //                                   + callbackUrl + "\">завершить регистрацию</a>");
        //}

        [HttpGet]
        public async Task<JsonResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return new JsonResult(new { answer = false });
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new JsonResult(new { answer = false });
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return new JsonResult(new { answer = true });
            else
                return new JsonResult(new { answer = false });
        }

        //Login users and geting tokens
        [HttpPost]
        [Route("token")]
        public async Task<JsonResult> GetToken(AuthenticationRequest authRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var signInRez = await signInManager.PasswordSignInAsync(authRequest.Name, authRequest.Password, false, false);

                    if (signInRez.Succeeded)
                    {
                        user = await userManager.FindByNameAsync(authRequest.Name);
                        var userRoles = await userManager.GetRolesAsync(user);
                        var userPermissions = await userManager.GetClaimsAsync(user);// get UserClaims(Permissions)

                        IList<IdentityRole> roles = new List<IdentityRole>();
                        foreach (var item in userRoles)
                        {
                            roles.Add(await roleManager.FindByNameAsync(item));
                        }

                        IList<Claim> rolePermissions = new List<Claim>();
                        foreach (var item in roles)
                        {
                            var claims = await roleManager.GetClaimsAsync(item);
                            foreach (var claim in claims)
                            {
                                rolePermissions.Add(claim); // get RoleClaims(Permissions)
                            }
                        }
                        var permissoins = userPermissions.Union(rolePermissions).ToList();// all permissions (role + user)

                        await userManager.RemoveAuthenticationTokenAsync(user, AuthOptions.ISSUER, "RefreshToken");
                        var refresh_jwtToken = await userManager.GenerateUserTokenAsync(user, AuthOptions.ISSUER, "RefreshToken");
                        await userManager.SetAuthenticationTokenAsync(user, AuthOptions.ISSUER, "RefreshToken", refresh_jwtToken);
                        string access_jwtToken = TokenFactory.GenerateAccessToken(user, permissoins);
                        log.LogInfo($"User {user.UserName} login successfully.");
                        return new JsonResult(new { access_jwtToken, refresh_jwtToken });
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"{ ex.Message } - { ex.StackTrace }");
            }
            return new JsonResult(new {result = "Invalid Data" });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ExternalLogin(string provider, string returnUrl = null)
        //{
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        //    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return new JsonResult(new { message = "LOG" });
        //}

        //[HttpGet]
        //public async Task<JsonResult> ExternalLoginCallback(string returnUrl = null)
        //{
        //    var info = await signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        return new JsonResult(new { message = "User not fount" });
        //    }

        //    var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //    if (signInResult.Succeeded)
        //    {
        //        return new JsonResult(new { message = "User login with google" });
        //    }
        //    if (signInResult.IsLockedOut)
        //    {
        //        return new JsonResult(new { message = "User data not valid" });
        //    }
        //    else
        //    {
        //        //ViewData["ReturnUrl"] = returnUrl;
        //        //ViewData["Provider"] = info.LoginProvider;
        //        //var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        //return View("ExternalLogin", new ExternalLoginModel { Email = email });
        //        return new JsonResult(new { info });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<JsonResult> ExternalLoginConfirmation(ExternalLoginModel model, string returnUrl = null)
        //{
        //    if (!ModelState.IsValid)
        //        return new JsonResult(new { mess = "Invalid data" });

        //    var info = await signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //        return new JsonResult(new { mess = "Invalid data" });

        //    var user = await userManager.FindByEmailAsync(model.Email);
        //    IdentityResult result;

        //    if (user != null)
        //    {
        //        result = await userManager.AddLoginAsync(user, info);
        //        if (result.Succeeded)
        //        {
        //            await signInManager.SignInAsync(user, isPersistent: false);
        //            return new JsonResult(new { mess = "Login seccidded" });
        //        }
        //    }
        //    else
        //    {
        //        model.Principal = info.Principal;
        //        user = new IdentityUser
        //        {
        //            UserName = model.Email
        //        };
        //        var config = new MapperConfiguration(cfg => cfg.CreateMap<IdentityUser, ExternalLoginModel>());
        //        var mapper = new Mapper(config);
        //        user = mapper.Map<IdentityUser>(model);
        //        result = await userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await userManager.AddLoginAsync(user, info);
        //            if (result.Succeeded)
        //            {
        //                //TODO: Send an emal for the email confirmation and add a default role as in the Register action
        //                await signInManager.SignInAsync(user, isPersistent: false);
        //                return new JsonResult(new { mess = "Registr and Login seccidded" }); ;
        //            }
        //        }
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.TryAddModelError(error.Code, error.Description);
        //    }

        //    return new JsonResult(new { model }); ;
        //}


        [HttpGet]
        [Route("LogOut")]
        public async void LogOut(string userName)
        {
            try
            {
                user = await userManager.FindByNameAsync(userName);
                await userManager.UpdateSecurityStampAsync(user);
                log.LogError($"User {user.UserName} logout.");
            }
            catch (Exception ex)
            {
                log.LogError($"{ ex.Message } - { ex.StackTrace }");
            }
        }

        // не проверяет на валидность userToken, принимает только RefreshToken и возвращвет пару "userToken + RefreshToken"
        [HttpPost("refreshUserToken")]
        public async Task<IActionResult> RefreshUserToken([FromBody] string RefreshToken)
        {
            try
            {
                var token = await context.UserTokens.FirstOrDefaultAsync(refT => refT.Value == RefreshToken);// проверка есть ли токен в базе

                if (token != null)
                {
                    user = await userManager.FindByIdAsync(token.UserId);

                    // new refresh token
                    await userManager.RemoveAuthenticationTokenAsync(user, AuthOptions.ISSUER, "RefreshToken");
                    var refresh_jwtToken = await userManager.GenerateUserTokenAsync(user, AuthOptions.ISSUER, "RefreshToken");
                    await userManager.SetAuthenticationTokenAsync(user, AuthOptions.ISSUER, "RefreshToken", refresh_jwtToken);

                    var userRoles = await userManager.GetRolesAsync(user);
                    var userPermissions = await userManager.GetClaimsAsync(user);// get UserClaims(Permissions)

                    IList<IdentityRole> roles = new List<IdentityRole>();
                    foreach (var item in userRoles)
                    {
                        roles.Add(await roleManager.FindByNameAsync(item));
                    }

                    IList<Claim> rolePermissions = new List<Claim>();
                    foreach (var item in roles)
                    {
                        var claims = await roleManager.GetClaimsAsync(item);
                        foreach (var claim in claims)
                        {
                            rolePermissions.Add(claim); // get RoleClaims(Permissions)
                        }
                    }
                    var permissoins = userPermissions.Union(rolePermissions).ToList();// all permissions (role + user)

                    string access_jwtToken = TokenFactory.GenerateAccessToken(user, permissoins);

                    return new JsonResult(new { access_jwtToken, refresh_jwtToken });
                }
                else
                {
                    return new JsonResult(new { result = "Invalid refresh token" });
                }
            }
            catch (Exception ex)
            {
                string mess = $"{ ex.Message } - { ex.StackTrace }";
                log.LogError(mess);
                return new JsonResult(new { mess });
            }
        }


        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        return new JsonResult(new { result = "Invalid email." });
                    }

                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Authentication", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);
                    emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Reset Password",
                        $"Follow the link: <a href='{url}'>link</a>");
                    return new JsonResult(new { result = url });
                }
                return new JsonResult(new { result = "Invalid email." });
            }
            catch (Exception ex)
            {
                string mess = $"{ ex.Message } - { ex.StackTrace }";
                log.LogError(mess);
                return new JsonResult(new { mess });
            }
        }


        [HttpPost("resetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return new JsonResult(new { result = "Invalid user." });
                }
                var result = await userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return new JsonResult(new { result = "Password was changed." });
                }
                string errors = null;
                foreach (var error in result.Errors)
                {
                    errors += error.Description;
                }
                return new JsonResult(new { result = errors });
            }
            catch (Exception ex)
            {
                string mess = $"{ ex.Message } - { ex.StackTrace }";
                log.LogError(mess);
                return new JsonResult(new { mess });
            }
        }
    }
}