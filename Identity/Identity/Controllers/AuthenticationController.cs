using Identity.Infrastructure;
using Identity.Models;
using Identity.Models.Requests;
using Microsoft.AspNetCore.Identity;
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

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private IdentityResult identityRez;
        private IdentityUser user;

        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
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
                    UserName = userData.Name,
                    Email = userData.Email
                };
                identityRez = await userManager.CreateAsync(user, userData.Password);

                if (identityRez.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return new JsonResult(new { answer = true });
                }
                return new JsonResult(new { identityRez });
            }
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

                    return new JsonResult(new { access_jwtToken, refresh_jwtToken });
                }
            }
            return new JsonResult(new { result = "Invalid Data" });
        }

        [HttpGet]
        [Route("LogOut")]
        public void LogOut()
        {
            userManager.UpdateSecurityStampAsync(user);
        }

        [HttpGet("refreshUserToken")]
        public async Task<IActionResult> RefreshUserToken([FromBody] RefreshUserTokenRequest request)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(request.Token, tokenValidationParameters, out var securityToken);
            if ((securityToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest("Invallid token");
            }

            var userName = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.Users.FirstOrDefaultAsync(u => (u.UserName == userName));
            var refToken = await userManager.GetAuthenticationTokenAsync(user, AuthOptions.ISSUER, "RefreshToken");

            if (refToken != request.RefreshToken)
            {
                return BadRequest("Invallid token");
            }

            //user.RefreshToken = GenerateRefreshToken();
            //await _userManager.UpdateAsync(user);
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
    }
}