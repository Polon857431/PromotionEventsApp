using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface ISpotService
    {
        Task AddSpot(SpotViewModel model);
        Task UpdateSpot(SpotViewModel model);
        Task<List<Spot>> List();
        Task<List<Spot>> UserSpots(User user);
        Task<List<Spot>> EventSpots(int eventId);
        Task EditSpot(SpotViewModel model);
        Task DeleteSpot(SpotViewModel model);
    }
}
