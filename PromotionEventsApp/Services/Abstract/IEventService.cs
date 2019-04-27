using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services.Abstract
{
    public interface IEventService
    {
        Task CreateEvent(EventViewModel model);
        Task<EventViewModel> GetEventViewModel(Guid id);
        Task UpdateEvent(Guid id, EventViewModel model);
        Task<List<EventViewModel>> List();


    }
}
