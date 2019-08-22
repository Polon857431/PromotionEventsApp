using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.DAL.Initializers
{
    public static class RoleInitalizer
    {
        private static readonly List<string> Roles = new List<string>()
        {
            "Admin",
            "User"
        };

        public static async Task Initalize(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles)
            {
                var roleInDb = await roleManager.FindByNameAsync(role);
                if (roleInDb == null)
                {
                    await roleManager.CreateAsync(new Role() { Name = role });
                }
          
            }
        }
    }
}