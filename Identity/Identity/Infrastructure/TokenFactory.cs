using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.Infrastructure
{
    public static class TokenFactory
    {
        public static string GenerateAccessToken(IdentityUser user/*, IdentityRole role*/, IList<Claim> permissions)
        {
            //var claims = new Claim[]
            //{
            //            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            //            new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name),
            //};
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var claimsData = new List<Claim> { new Claim(type: ClaimTypes.NameIdentifier, user.UserName)/*, new Claim(type: ClaimTypes.Role, role.Name)*/ };
            //generate Claim permissions
            for (int i = 0; i < permissions.Count; i++)
            {
                claimsData.Add(permissions[i]);
            }

            var access_token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claimsData,
                expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(access_token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
