using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PromotionEventsApp.Helpers;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTConfiguration _jwtConfiguration;

        public TokenService(IOptions<JWTConfiguration>  jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration.Value;
                ;
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

        public List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };
        }
    }
}
