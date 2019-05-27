using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserPanelController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> UserEvents()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _eventService.UserEvents(user);
            return View(result);
        }
    }
}