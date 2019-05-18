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
    public class SpotService : ISpotService
    {
        private readonly ISpotRepository _spotRepository;

        public SpotService(ISpotRepository spotRepository)
        {
            _spotRepository = spotRepository;
        }

        public async Task AddSpot(SpotViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSpot(SpotViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Spot>> List()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Spot>> UserSpots(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Spot>> EventSpots(int eventId)
        {
            throw new NotImplementedException();
        }

        public async Task EditSpot(SpotViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSpot(SpotViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
