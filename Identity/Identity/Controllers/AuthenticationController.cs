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

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private IdentityResult identityRez;
        private IdentityUser user;
        private ILoggerManager log;

        public AuthenticationController(UserContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, ILoggerManager log)
        {
            this._context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.log = log;
        }

        // Registration users
        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> PostRegistration([FromBody] RegisterBindingRequest userData)
        {
            if (ModelState.IsValid)
            {
                //IdentityUser user = await userManager.FindByNameAsync("test");
                //await userManager.DeleteAsync(user);

                user = new IdentityUser()
                {
                    UserName = userData.Email,
                    Email = userData.Email
                };
                log.LogInfo($"New user {user.UserName} registration.");
                identityRez = await userManager.CreateAsync(user, userData.Password);

                if (identityRez.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "RegisteredUser");
                    return new JsonResult(new { answer = true });
                }
                log.LogError(identityRez.Errors.ToString());
                return new JsonResult(new { identityRez });
            }
            log.LogError("Registration - ModelState is not valid.");
            return new JsonResult(new { mes = "Error" });
        }

        //Login users and geting tokens
        [HttpPost]
        [Route("token")]
        public async Task<JsonResult> GetToken(AuthenticationRequest authRequest)
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
            log.LogError("Login - ModelState is not valid.");
            return new JsonResult(new {result = "Invalid Data" });
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public async Task<JsonResult> ExternalLogin(string provider)
        //{
        //    string returnUrl = null;
        //    var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
        //    var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        //    return new JsonResult(new { message = "LOG" });
        //}

        [HttpGet]
        public async Task<JsonResult> ExternalLoginCallback(string returnUrl = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return new JsonResult(new { message = "User not fount" });
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return new JsonResult(new { message = "User login with google" });
            }
            if (signInResult.IsLockedOut)
            {
                return new JsonResult(new { message = "User data not valid" });
            }
            else
            {
                //ViewData["ReturnUrl"] = returnUrl;
                //ViewData["Provider"] = info.LoginProvider;
                //var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //return View("ExternalLogin", new ExternalLoginModel { Email = email });
                return new JsonResult(new { info });
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> ExternalLoginConfirmation(ExternalLoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { mess = "Invalid data" });

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return new JsonResult(new { mess = "Invalid data" });

            var user = await userManager.FindByEmailAsync(model.Email);
            IdentityResult result;

            if (user != null)
            {
                result = await userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return new JsonResult(new { mess = "Login seccidded" });
                }
            }
            else
            {
                model.Principal = info.Principal;
                user = new IdentityUser
                {
                    UserName = model.Email
                };
                var config = new MapperConfiguration(cfg => cfg.CreateMap<IdentityUser, ExternalLoginModel>());
                var mapper = new Mapper(config);
                user = mapper.Map<IdentityUser>(model);
                result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        //TODO: Send an emal for the email confirmation and add a default role as in the Register action
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return new JsonResult(new { mess = "Registr and Login seccidded" }); ;
                    }
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return new JsonResult(new { model }); ;
        }


        [HttpGet]
        [Route("LogOut")]
        public async void LogOut(string userName)
        {
            user = await userManager.FindByNameAsync(userName);
            await userManager.UpdateSecurityStampAsync(user);
        }

        // не проверяет на валидность userToken, принимает только RefreshToken и возвращвет пару "userToken + RefreshToken"
        [HttpPost("refreshUserToken")]
        public async Task<IActionResult> RefreshUserToken([FromBody] string RefreshToken)
        {
            var token = await _context.UserTokens.FirstOrDefaultAsync(refT => refT.Value == RefreshToken);// проверка есть ли токен в базе

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
                log.LogError($"Invalid refresh token");
                return new JsonResult(new { result = "Invalid refresh token" });
            }
        }


        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
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
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(model.Email, "Reset Password",
                    $"Follow the link: <a href='{url}'>link</a>");
                return new JsonResult(new { result = url });
            }
            return new JsonResult(new { result = "Invalid email." });
        }


        [HttpPost("resetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
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
    }
}