using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;

        public EventController(IEventService eventService, UserManager<User> userManager)
        {
            _eventService = eventService;
            _userManager = userManager;
        }

        #region CreateEvent
        [HttpGet]

        public IActionResult CreateEvent()
        {

            return View(new EventViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _eventService.CreateEvent(model);
            return View();
        }
        #endregion

        #region Event Details

        public async Task<IActionResult> Details(int id)
        {
            return View(await _eventService.GetEventViewModel(id));
        }

        #endregion
        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _eventService.GetEventViewModel(id));
        }
        public async Task<IActionResult> Edit(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _eventService.UpdateEvent(model);
            return RedirectToAction("Details");
        }
        #endregion
        #region List
        public async Task<IActionResult> List()
        {
            var e = await _eventService.List();
            return View(await _eventService.List());
        }
        #endregion

        public async Task<IActionResult> JoinToEvent(int eventId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            await _eventService.JoinToEvent(eventId, user);
            return Json(new {success = true});
        }



    }



}
