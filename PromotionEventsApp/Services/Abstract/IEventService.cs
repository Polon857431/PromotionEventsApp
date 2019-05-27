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
        int GetNewId();
        Task JoinToEvent(int eventId, User user);
        Task LeaveEvent(int eventId, User user);
        Task<List<UserEventsViewModel>> UserEvents(User user);
        Task<List<User>> EventMembers(int eventId);
        




    }
}
