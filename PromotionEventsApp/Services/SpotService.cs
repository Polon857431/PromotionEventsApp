﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IEventService _eventService;

        public SpotService(ISpotRepository spotRepository, IMapper mapper, IEventService eventService)
        {
            _spotRepository = spotRepository;
            _mapper = mapper;
            _eventService = eventService;
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
    }
}
