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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace PromotionEventsApp.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ISpotRepository _spotRepository;
        private readonly IMemberRepository _memberRepository;

        public EventService(IEventRepository eventRepository, IMapper mapper, IHostingEnvironment hostingEnvironment, ISpotRepository spotRepository, IMemberRepository memberRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _spotRepository = spotRepository;
            _memberRepository = memberRepository;
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

            var result = _mapper.Map<Event, EventViewModel>(e);
            result.Members = await EventMembers(id);
            return result;
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


        public async Task<List<UserEventsViewModel>> UserEvents(User user)
        {
            List<UserEventsViewModel> result = new List<UserEventsViewModel>();
            var e = await _eventRepository.GetUserEvents(user);


            foreach (var element in e)
            {

                var el = new UserEventsViewModel
                {
                    Event = _mapper.Map<Event, EventViewModel>(element.Event),
                    User = user,
                   // UserSpots = 
                };

             //   el.Event.Spots = await .EventSpots(element.EventId);


                result.Add(el);

            }

            return result;

        }

        public async Task<List<User>> EventMembers(int eventId)
        {
            var e = await _eventRepository.GetEventMembers(eventId);
            return e.Select(_ => _.User).ToList();
        }

        public async Task<List<Event>> GetClosestEvents(int count)
        {
            var result = await _eventRepository.GetAllAsync();
            return result.Where(_ => _.StartTime > DateTime.Now).OrderBy(_=>_.StartTime).Take(count).ToList();
           
        }

        public async Task<List<Event>> GetActualEvents()
        {
            var result = await _eventRepository.FindByAsync(
                _ => _.StartTime < DateTime.Now && _.EndTime > DateTime.Now);

            return result.ToList();

        }


        public async Task JoinToEvent(int eventId, User user)
        {
            var e = await _eventRepository.GetAsync(eventId);
            _memberRepository.Add(new Member() { Event = e, User = user });
            await _memberRepository.CommitAsync();
        }

        public async Task LeaveEvent(int eventId, User user)
        {
            var e = await _eventRepository.GetAsync(eventId);
            e.Members.Remove(await _eventRepository.GetMember(eventId, user));
            user.Events.Remove(await _eventRepository.GetMember(eventId, user));
            _eventRepository.Update(e);
            await _eventRepository.CommitAsync();

        }


    }
}

