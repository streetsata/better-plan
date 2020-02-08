using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Models;
using Identity.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        private enum Roles
        {
            Admin,
            User
        }
        private enum Permissoins
        {
            Look,
            Change
        }

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, UserContext context)
        {
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
            model.roles.Add("User");
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

                    return new JsonResult(200);
                }
            }

            return NotFound();
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

                return NotFound();
            }
            else
                return BadRequest("Invallid permission");
        }

        //[HttpPost("addPermissionForRole")]
        //public async Task<IActionResult> AddPermissionForRole([FromBody] AddPermissionsRequest model)
        //{
        //    bool check = true;
        //    foreach (var item in model.claims)
        //    {
        //        if (!Enum.IsDefined(typeof(Permissoins), item))
        //        {
        //            check = false;
        //        }
        //    }
        //    if (check)
        //    {
        //        // получаем роль
        //        var role = await _roleManager.FindByIdAsync(model.id);
        //        if (role != null)
        //        {
        //            var claims = new List<Claim>();
        //            for (int i = 0; i < model.claims.Count; i++)
        //            {
        //                claims.Add(new Claim(type: "Permission", model.claims[i]));
        //            }

        //            foreach (var item in claims)
        //            {
        //                await _roleManager.AddClaimAsync(role, item);
        //            }

        //            return new JsonResult(200);
        //        }

        //        return NotFound();
        //    }
        //    else
        //        return BadRequest("Invallid permission");
        //}
    }
}