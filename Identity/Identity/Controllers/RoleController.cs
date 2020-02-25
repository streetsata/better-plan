using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Contracts;
using Identity.Models;
using Identity.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        UserContext _context;
        IdentityUser user;
        private ILoggerManager log;
        private enum Roles
        {
            Admin,
            Manager,
            Visual,
            Targetologist,
            Copywriter,
            RegisteredUser
        }
        private enum Permissoins
        {
            Work_With_Publications,
            Work_With_AdvertisingCompany,
            Work_With_Scheduler,
            Posting,
            Work_With_Drafts,
            Work_With_Project,
            Work_With_Team,
            Registered_User_Work
        }

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, UserContext context, ILoggerManager log)
        {
            this.log = log;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("allRoles")]
        [HttpGet]
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        [HttpPut("editUserRoles")]
        public async Task<IActionResult> Edit([FromBody] EditUserRolesRequest model)
        {
            // никогда не убирать роль User
            model.roles.Add("RegisteredUser");
            bool check = true;
            foreach (var item in model.roles)
            {
                if (!Enum.IsDefined(typeof(Roles), item))
                {
                    check = false;
                }
            }
            if (check)
            {
                // получаем пользователя
                user = await _userManager.FindByIdAsync(model.id);
                if (user != null)
                {
                    // получем список ролей пользователя
                    var userRoles = await _userManager.GetRolesAsync(user);
                    // получаем список ролей, которые были добавлены
                    var addedRoles = model.roles.Except(userRoles);
                    // получаем роли, которые были удалены
                    var removedRoles = userRoles.Except(model.roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    log.LogInfo($"Change rol for user {user.UserName}");
                    return new JsonResult(200);
                }
                else
                {
                    log.LogError("User was not found");
                    return new JsonResult(new { result = "User was not found" });
                }
            }
            else
            {
                log.LogError("Invalid role");
                return new JsonResult(new { result = "Invalid role" });
            }
        }

        [HttpPut("editUserPermissions")]
        public async Task<IActionResult> EditUserPermissions([FromBody] EditUserPermissionsRequest model)
        {
            bool check = true;
            foreach (var item in model.claims)
            {
                if (!Enum.IsDefined(typeof(Permissoins), item))
                {
                    check = false;
                }
            }
            if (check)
            {
                user = await _userManager.FindByIdAsync(model.id);
                if (user != null)
                {
                    // получаем список всех разрешений пользователя
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    List<string> userClaimsValue = new List<string>();
                    foreach (var item in userClaims)
                    {
                        userClaimsValue.Add(item.Value);
                    }
                    // получаем список добавленных разрешений
                    var addedClaimsValue = model.claims.Except(userClaimsValue).ToList();
                    // получаем список удаленных разрешений
                    var removedClaimsValue = userClaimsValue.Except(model.claims).ToList();

                    var addedClaims = new List<Claim>();
                    for (int i = 0; i < addedClaimsValue.Count; i++)
                    {
                        addedClaims.Add(new Claim(type: "Permission", addedClaimsValue[i]));
                    }
                    var removedClaims = new List<Claim>();
                    for (int i = 0; i < removedClaimsValue.Count; i++)
                    {
                        removedClaims.Add(new Claim(type: "Permission", removedClaimsValue[i]));
                    }

                    await _userManager.AddClaimsAsync(user, addedClaims);
                    await _userManager.RemoveClaimsAsync(user, removedClaims);

                    return new JsonResult(200);
                }
                log.LogError("User was not found");
                return new JsonResult(new { result = "User was not found" });
            }
            else
            {
                log.LogError("Invalid permission");
                return new JsonResult(new { result = "Invalid permission" });
            }
        }












        [HttpPost("addPermissionForRole")]
        public async Task<IActionResult> AddPermissionForRole([FromBody] EditUserPermissionsRequest model)
        {
            bool check = true;
            foreach (var item in model.claims)
            {
                if (!Enum.IsDefined(typeof(Permissoins), item))
                {
                    check = false;
                }
            }
            if (check)
            {
                // получаем роль
                var role = await _roleManager.FindByIdAsync(model.id);
                if (role != null)
                {
                    var claims = new List<Claim>();
                    for (int i = 0; i < model.claims.Count; i++)
                    {
                        claims.Add(new Claim(type: "Permission", model.claims[i]));
                    }

                    foreach (var item in claims)
                    {
                        await _roleManager.AddClaimAsync(role, item);
                    }

                    return new JsonResult(200);
                }

                return NotFound();
            }
            else
                return BadRequest("Invallid permission");
        }
    }
}