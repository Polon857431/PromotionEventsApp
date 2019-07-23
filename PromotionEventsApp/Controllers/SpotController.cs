using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Helpers;
using PromotionEventsApp.Models;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Controllers
{
    public class SpotController : Controller
    {
        private readonly ISpotService _spotService;

        public SpotController(ISpotService spotService)
        {
            _spotService = spotService;
        }

        // GET: Spot
        public ActionResult Index()
        {
            return View();
        }

        // GET: Spot/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var spot = await _spotService.GetSpot(id);
            ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(QrGenerator.GetCode(spot.Id+"_"+spot.Name,20));
            return View(spot);
        }

        // GET: Spot/Create
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Spot/Create
        [HttpPost]
        public async Task<IActionResult> Create(SpotViewModel model)
        {
            await _spotService.Create(model);
            return View();
        }

        // GET: Spot/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Spot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Spot/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Spot/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}