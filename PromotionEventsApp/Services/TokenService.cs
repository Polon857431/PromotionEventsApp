using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PromotionEventsApp.Helpers;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;

        public TokenService(IOptions<JwtConfiguration> jwtConfiguration, UserManager<User> userManager)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtConfiguration.Value;
            
        }

        public string GenerateToken(List<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfiguration.Secret);
            var jwToken = new JwtSecurityToken(
                // issuer: "http://localhost:44369/",
                // audience: "http://localhost:44369/",
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwToken);
        }

        public async Task<List<Claim>> GetUserClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var result = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };

            foreach (var role in roles)
            {
                result.Add(new Claim(ClaimTypes.Role, role));
            }

            return result.ToList();
        }

        public async Task<bool> CheckUserPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> Auth(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (await CheckUserPassword(user, password))
            {
                return GenerateToken(await GetUserClaims(user));
            }

            return null;
        }



    }
}
