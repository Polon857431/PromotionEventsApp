using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;

namespace PromotionEventsApp.Services
{
    public class SpotService : ISpotService
    {
        private readonly ISpotRepository _spotRepository;
        private readonly IMapper _mapper;

        private readonly IEventRepository _eventRepository;

        public SpotService(ISpotRepository spotRepository, IMapper mapper, IEventService eventService, IEventRepository eventRepository)
        {
            _spotRepository = spotRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task AddSpot(SpotViewModel model)
        {
            var s = _mapper.Map<SpotViewModel, Spot>(model);
            s.Id = _spotRepository.GetLastId() + 1;
            _spotRepository.Add(s);
            await _spotRepository.CommitAsync();
        }

        public async Task UpdateSpot(SpotViewModel model)
        {
            var s = _mapper.Map<SpotViewModel, Spot>(model);
            _spotRepository.Update(s);
            await _spotRepository.CommitAsync();
        }

        public async Task<List<Spot>> List()
        {
            var result = await _spotRepository.GetAllAsync();
            return result.ToList();
        }

        public async Task<List<Spot>> UserSpots(User user)
        {
            var result = await _spotRepository.GetUserSpots(user);
            return result.Select(_ => _.Spot).ToList();

        }

        public async Task<List<Spot>> EventSpots(int eventId)
        {
            var result = await _eventRepository.GetEventSpots(eventId);
            return result.Select(_ => _.Spot).ToList();
        }

        public async Task EditSpot(SpotViewModel model)
        {
            _spotRepository.Update(_mapper.Map<SpotViewModel, Spot>(model));
            await _spotRepository.CommitAsync();
        }

        public async Task DeleteSpot(SpotViewModel model)
        {
            _spotRepository.Delete(_mapper.Map<SpotViewModel, Spot>(model));
            await _spotRepository.CommitAsync();
        }

        public async Task<AddSpotToEventViewModel> GetAddSpotToEventViewModel(int eventId)
        {
            List<Spot> availableSpots = await _spotRepository.GetAllAsync() as List<Spot>;

            var eventSpots = await EventSpots(eventId);
            return new AddSpotToEventViewModel()
            {
                EventId = eventId,
                EventSpots = eventSpots,
                AvailableSpots = (availableSpots ?? throw new InvalidOperationException()).Except(eventSpots).ToList()
            };

        }

        public async Task<Spot> GetSpot(int id)
        {
            return await _spotRepository.GetAsync(id);
        }
    }
}
