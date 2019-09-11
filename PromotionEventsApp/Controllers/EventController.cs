using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PromotionEventsApp.Helpers;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<User> _userManager;
        private readonly ISpotService _spotService;


        public EventController(IEventService eventService, UserManager<User> userManager, ISpotService spotService)
        {
            _eventService = eventService;
            _userManager = userManager;
            _spotService = spotService;
        }

        #region CreateEvent
        [HttpGet]

        public IActionResult CreateEvent() => View(new EventViewModel());


        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _eventService.CreateEvent(model);
            }
            catch
            {

                TempData["notifications"] = JsonConvert.SerializeObject(
                    new List<Notification>
                    {
                        NotificationGenerator.CreateNotification(
                            "Dodanie wydarzenia",
                            "Nie udało się stworzyć wydarzenia",
                            NotificationType.Danger,
                            "fas fa-close",
                            3000)
                    });
                return View();
            }

            TempData["notifications"] = JsonConvert.SerializeObject(
                new List<Notification>
                {
                    NotificationGenerator.CreateNotification(
                    "Dodanie wydarzenia",
                    "Wydarzenie " + model.Name + "zostalo pomyślnie dodane",
                    NotificationType.Success,
                    "fas fa-plus",
                    3000)

                });
            return View();
        }
        #endregion

        #region Event Details

        public async Task<IActionResult> Details(int id) =>
            View(await _eventService.GetEventViewModel(id));


        #endregion
        #region Edit

        public async Task<IActionResult> Edit(int id) =>
            View(await _eventService.GetEventViewModel(id));

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
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> AddSpotToEvent(int eventId) =>
            View(await _spotService.GetAddSpotToEventViewModel(eventId));



    }



}
