using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index() => View(new Tuple<List<Event>, List<Event>>
                    (await _eventService.GetClosestEvents(5), await _eventService.GetActualEvents()));


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}