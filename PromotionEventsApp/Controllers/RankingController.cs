using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Controllers
{
    public class RankingController : Controller
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        public async Task<IActionResult> Ranking() =>
            View(await _rankingService.GlobalRanking());

        public async Task<IActionResult> EventRanking(int eventId) => 
            View(await _rankingService.EventRank(eventId));

    }
}