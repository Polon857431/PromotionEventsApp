using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.DAL.Initializers
{
    public static class InitalizersLoader
    {
        public static async Task LoadInitializers(RoleManager<Role> roleManager, UserManager<User> userManager , IConfiguration configuration)
        {
           await RoleInitalizer.Initalize(roleManager);
           await UserInitializer.CreateAdminAccount(userManager, roleManager, configuration);
           await UserInitializer.CreateTestAccount(userManager, roleManager, configuration);
        }
    }
}
