using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Controllers
{
    public class UserPanelController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;

        public UserPanelController(IEventService eventService, UserManager<User> userManager)
        {
            _eventService = eventService;
            _userManager = userManager;
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

        public IActionResult ChangePersonalData()
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