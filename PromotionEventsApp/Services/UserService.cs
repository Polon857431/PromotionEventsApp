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
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


    

        public async Task ChangePersonalData(UserPersonalDataViewModel model, User user)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model, User user)
        {
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            await _userManager.UpdateAsync(user);
            return result;

        }

        public async Task ChangeEmail(ChangeEmailViewModel model, User user)
        {
            if (await _userManager.CheckPasswordAsync(user, model.Password) && user.Email.Equals(model.Email))
            {
                user.Email = model.Email;
                await _userManager.UpdateAsync(user);

            }
            else
            {
                //todo
            }

        }
    }
}
