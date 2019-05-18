using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
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



    }



}
