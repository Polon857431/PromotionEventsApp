using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.Services.Abstract
{
    public interface ITokenService
    {
        string GenerateToken(List<Claim> claims);
        List<Claim> GetUserClaims(User user);
    }
}
