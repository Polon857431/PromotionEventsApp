using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;
using PromotionEventsApp.Services.Abstract;
using PromotionEventsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace PromotionEventsApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EventService(IEventRepository eventRepository, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task CreateEvent(EventViewModel model)
        {
            var e = _mapper.Map<EventViewModel, Event>(model);
            e.Id = GetNewId();
            if (model.EventImage != null && model.EventImage.ContentType.Contains("image"))
            {
                e.Image = UploadEventPhoto(model.EventImage, e.Id);

            }
            _eventRepository.Add(e);
            await _eventRepository.CommitAsync();

        }

        public async Task<EventViewModel> GetEventViewModel(int id)
        {
            var e = await _eventRepository.GetAsync(id);
            return _mapper.Map<Event, EventViewModel>(e);
        }

        public async Task<Event> GetEvent(int id)
        {
            return await _eventRepository.GetAsync(id, _ => _.Spots, _ => _.Members);
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
            await _eventRepository.CommitAsync();

        }

        public async Task<List<Event>> List()
        {
            var list = await _eventRepository.GetAllAsync();
            return list.ToList();
        }

        public string UploadEventPhoto(IFormFile formFile, int eventId)
        {
            string newPath = Path.Combine(_hostingEnvironment.WebRootPath, "Events", eventId.ToString());
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            FileInfo uploadedFileInfo = new FileInfo(formFile.FileName);
            var fileName = $"{Guid.NewGuid()}{uploadedFileInfo.Extension}";
            using (var stream = new FileStream(Path.Combine(newPath, fileName), FileMode.Create))
            {
                formFile.CopyTo(stream);

            }

            return fileName;


        }

        public int GetNewId() => _eventRepository.GetLastId() + 1;


        public async Task<List<Event>> UserEvents(User user)
        {
            var e = await _eventRepository.GetUserEvents(user);
            return e.Select(_ => _.Event).ToList();
        }

        public async Task<List<User>> EventMembers(int eventId)
        {
            var e = await _eventRepository.GetEventMembers(eventId);
            return e.Select(_ => _.User).ToList();
        }



        public async Task JoinToEvent(int eventId, User user)
        {
            var e = await _eventRepository.GetAsync(eventId);
            e.Members.Add(new Member() { Event = e, User = user });
            _eventRepository.Update(e);
            await _eventRepository.CommitAsync();
        }


    }
}

