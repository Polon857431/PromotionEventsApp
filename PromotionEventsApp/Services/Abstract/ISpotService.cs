using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface ISpotService
    {
        Task AddSpot(CreateSpotViewModel model);
        Task UpdateSpot(SpotViewModel model);
        Task<List<Spot>> List();
        Task<List<Spot>> UserSpots(User user);
        Task<List<Spot>> EventSpots(int eventId);
        Task EditSpot(SpotViewModel model);
        Task DeleteSpot(SpotViewModel model);
        Task<AddSpotToEventViewModel> GetAddSpotToEventViewModel(int eventId);
        Task<Spot> GetSpot(int id);
        Task Create(CreateSpotViewModel model);
        Task<List<Spot>> GetAllSpots();
    }
}
