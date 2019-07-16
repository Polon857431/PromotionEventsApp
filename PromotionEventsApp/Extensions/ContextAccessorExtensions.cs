using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PromotionEventsApp.Extensions
{
    public static class ContextAccessorExtensions
    {
        public static int GetUserId(this IHttpContextAccessor accessor)
        {
            int.TryParse(accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value, out var result);
            return result;
        }
    }
}
