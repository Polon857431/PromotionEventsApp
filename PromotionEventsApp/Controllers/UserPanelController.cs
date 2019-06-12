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

        public async Task<IActionResult> ChangePersonalData()
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePersonalData(UserPersonalDataViewModel model)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            throw new System.NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswoeed(ChangePasswordViewModel model)
        {
            throw new System.NotImplementedException();
        }



        public IActionResult ChangeEmail()
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