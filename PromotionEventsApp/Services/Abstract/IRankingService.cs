using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface IRankingService
    {
        Task<List<RankViewModel>> GlobalRanking();
        Task<List<RankViewModel>> EventRank(int eventId);
        Task<RankViewModel> UserGlobalRank(User user);
        Task<RankViewModel> UserEventRank(User user, int eventId);


    }
}
