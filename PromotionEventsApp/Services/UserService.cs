using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task ChangePersonalData(UserPersonalDataViewModel model, User user)
        {
           await _userManager.UpdateAsync(user);
        }

        public async Task ChangePassword(ChangePasswordViewModel model, User user)
        {
            await _userManager.UpdateAsync(user);

        }

        public async Task ChangeEmail(ChangeEmailViewModel model, User user)
        {
            await _userManager.UpdateAsync(user);

        }
    }
}
