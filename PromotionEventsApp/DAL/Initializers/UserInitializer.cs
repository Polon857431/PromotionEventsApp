using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.DAL.Initializers
{
    public class UserInitializer
    {
        public static async Task CreateAdminAccount(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            var superAdmin = new User()
            {
                UserName = configuration["SuperAdmin:UserName"],
                FirstName = configuration["SuperAdmin:FirstName"],
                LastName = configuration["SuperAdmin:LastName"],
                Email = configuration["SuperAdmin:Email"],
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync(superAdmin.Email) == null)
            {
                await userManager.CreateAsync(superAdmin, configuration["SuperAdmin:Password"]);
                await userManager.AddToRolesAsync(superAdmin, roleManager.Roles.Select(_ => _.Name).ToList());
            }
        }

        public static async Task CreateTestAccount(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
        {
            var testUser = new User()
            {
                UserName = configuration["TestUser:UserName"],
                FirstName = configuration["TestUser:FirstName"],
                LastName = configuration["TestUser:LastName"],
                Email = configuration["TestUser:Email"],
                EmailConfirmed = true
            };
            if (await userManager.FindByEmailAsync(testUser.Email) == null)
            {
                await userManager.CreateAsync(testUser, configuration["TestUser:Password"]);
                await userManager.AddToRoleAsync(testUser, "User");
            }
        }
    }
}

