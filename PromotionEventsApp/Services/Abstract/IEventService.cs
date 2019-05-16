using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface IEventService
    {
        Task CreateEvent(EventViewModel model);
        Task<EventViewModel> GetEventViewModel(int id);
        Task UpdateEvent(EventViewModel model);
        Task<List<Event>> List();


    }
}
