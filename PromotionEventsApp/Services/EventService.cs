using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace PromotionEventsApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task CreateEvent(EventViewModel model)
        {
            _eventRepository.Add(_mapper.Map<EventViewModel, Event>(model));
             await _eventRepository.CommitAsync();

        }

        public async Task<EventViewModel> GetEventViewModel(int id)
        {
            var e = await _eventRepository.GetAsync(id);
            return _mapper.Map<Event, EventViewModel>(e);
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _eventRepository.GetAsync(id, _ => _.Spots);
        }

        public async Task UpdateEvent(EventViewModel model)
        {
            var e = _mapper.Map<EventViewModel, Event>(model);
            _eventRepository.Update(e);
            await _eventRepository.CommitAsync();

        }

        public async Task AddSpot(int eventId, int spotId)
        {
            Event e = await _eventRepository.GetAsync(eventId, _ => _.Spots);
            EventSpot es = new EventSpot() { EventId = eventId, SpotId = spotId };
            e.Spots.Add(es);
            _eventRepository.Update(e);

        }

        public async Task<List<Event>> List()
        {
            List<EventViewModel> result = new List<EventViewModel>();
            var list = await _eventRepository.GetAllAsync();
            return list.ToList();
        }

    }
}

