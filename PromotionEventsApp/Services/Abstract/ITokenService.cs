using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.Services.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(List<Claim> claims);
        Task<List<Claim>> GetUserClaims(User user);
        Task<bool> CheckUserPassword(User user, string password);
        Task<string> Auth(string username, string password);
    }
}
