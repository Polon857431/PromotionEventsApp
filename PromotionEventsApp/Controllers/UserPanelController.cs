using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserPanelController(IEventService eventService, UserManager<User> userManager, IUserService userService)
        {
            _eventService = eventService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> UserEvents()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _eventService.UserEvents(user);
            return View(result);
        }

        public async Task<IActionResult> Index()
        {

            return View(await _userManager.FindByNameAsync(User.Identity.Name));
        }

        #region ChangePerosnalData
        [HttpGet]
        public async Task<IActionResult> ChangePersonalData()
        {
            return View(_userService.GetPersonalDataViewModel(await _userManager.FindByNameAsync(User.Identity.Name)));
        }

        [HttpPost]
        public async Task<IActionResult> ChangePersonalData(UserPersonalDataViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userService.ChangePersonalData(model, await _userManager.FindByNameAsync(User.Identity.Name));
            return RedirectToAction("Index");
        }
        #endregion


        #region ChangeEmail
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            throw new System.NotImplementedException();
        }



        public IActionResult ChangeEmail()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public IActionResult ChangePassword()
        {
            throw new System.NotImplementedException();
        }

        public IActionResult UserRank()
        {
            throw new System.NotImplementedException();
        }
    }
}