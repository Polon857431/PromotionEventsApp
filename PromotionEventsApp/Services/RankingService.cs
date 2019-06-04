using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services
{
    public class RankingService : IRankingService
    {
        private readonly IRankRepository _rankRepository;

        public RankingService(IRankRepository rankRepository)
        {
            _rankRepository = rankRepository;
        }

        public async Task<List<RankViewModel>> GlobalRanking()
        {
            var result = new List<RankViewModel>();
            var res = await _rankRepository.GetAllAsync(_ => _.User);
            var visitedSpots = res.ToList();
            foreach (var element in visitedSpots)
            {
                if (!result.Exists(_ => _.User.Id == element.UserId))
                {
                    result.Add(new RankViewModel
                    {
                        Points = visitedSpots.Where(_ => _.UserId == element.UserId).ToList().Sum(_ => _.Value),
                        User = element.User
                    });
                }
            }

            return result.OrderBy(_ => _.Points).ToList();
        }

        public async Task<List<RankViewModel>> EventRank(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task<RankViewModel> UserGlobalRank(User user)
        {
            var res = await GlobalRanking();
            var result = res.SingleOrDefault(_ => _.User.Id == user.Id) ?? throw new ArgumentNullException("res.SingleOrDefault(_ => _.User.Id == user.Id)");
            result.Id = res.IndexOf(result);
            return result;
        }

        public async Task<RankViewModel> UserEventRank(User user, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}