﻿using Identity.Infrastructure;
using Identity.Models;
using Identity.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Authorization;

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
        [HttpGet]
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
                var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);
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