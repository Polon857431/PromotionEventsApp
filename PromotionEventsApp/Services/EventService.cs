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
            Event e = new Event();
            _eventRepository.Add(e);
            await _eventRepository.CommitAsync();

        }

        public async Task<EventViewModel> GetEventViewModel(Guid id)
        {
            var e = await _eventRepository.GetAsync(id);

            return new EventViewModel();
        }

        public async Task UpdateEvent(Guid id, EventViewModel model)
        {
            Event e = await _eventRepository.GetAsync(id);
            //to do 
            _eventRepository.Update(e);
            throw new NotImplementedException();
        }

        public async Task<List<EventViewModel>> List()
        {
            List<EventViewModel> result = new List<EventViewModel>();
            var events = await _eventRepository.GetAllAsync();
            foreach (var e in events)
            {
                result.Add(await GetEventViewModel(e.Id));
            }

            throw new NotImplementedException();
        }
    }
}

