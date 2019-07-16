using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Newtonsoft.Json;
using PromotionEventsApp.Extensions;
using PromotionEventsApp.Helpers;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;

        public UserPanelController(IEventService eventService, UserManager<User> userManager, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _userManager = userManager;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> UserEvents()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email).Value);
            var result = await _eventService.UserEvents(user);
            return View(result);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.Users.SingleAsync(_=>_.Id == _httpContextAccessor.GetUserId());
            return View(user);
        }

        #region ChangePerosnalData
        [HttpGet]
        public async Task<IActionResult> ChangePersonalData()
        {
            return View(_userService.GetPersonalDataViewModel(await _userManager.FindByIdAsync(_httpContextAccessor.GetUserId().ToString())));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePersonalData(UserPersonalDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userService.ChangePersonalData(model, await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value));

            return RedirectToAction("Index");
        }
        #endregion


        #region ChangeEmail




        public IActionResult ChangeEmail()
        {
            throw new System.NotImplementedException();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var _ in ModelState)
                {
                    foreach (var __ in _.Value.Errors)
                    {
                        sb.Append(__.ErrorMessage);
                    }
                }
                TempData["notifications"] = JsonConvert.SerializeObject(
                    new List<Notification>
                    {
                        NotificationGenerator.CreateNotification(
                            "Zmiana hasłą",
                            sb.ToString(),
                            NotificationType.Danger,
                            "fas fa-close",
                            3000)
                    });
                return View();

            }

            var result = await _userService.ChangePassword(model, await _userManager.FindByNameAsync(User.Identity.Name));
            if (result.Succeeded)
            {
                TempData["notifications"] = JsonConvert.SerializeObject(
                    new List<Notification>
                    {
                        NotificationGenerator.CreateNotification(
                            "Zmiana hasłą",
                            "Hasło zostało zmiennione pomyślnie",
                            NotificationType.Success,
                            "fas fa-close",
                            3000)
                    });
            }
            else
            {
                TempData["notifications"] = JsonConvert.SerializeObject(
                    new List<Notification>
                    {
                        NotificationGenerator.CreateNotification(
                            "Zmiana hasłą",
                            "Zmiana hasła się nie powiodłą",
                            NotificationType.Warning,
                            "fas fa-close",
                            3000)
                    });
            }

            return View();
        }

        //public IActionResult UserRank()
        //{
            
        //}
    }
}