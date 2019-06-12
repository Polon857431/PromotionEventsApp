using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface IUserService
    {
        Task ChangePersonalData(UserPersonalDataViewModel model, User user);
        Task ChangePassword(ChangePasswordViewModel model, User user);
        Task ChangeEmail(ChangeEmailViewModel model, User user);

    }
}
