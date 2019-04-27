using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromotionEventsApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task CreateEvent(EventViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<EventViewModel> GetEventViewModel(Guid id)
        {
           
           return new EventViewModel();
        }

        public async Task UpdateEvent(Guid id, EventViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventViewModel>> List()
        {
            throw new NotImplementedException();
        }
    }
}

