using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public SpotService(ISpotRepository spotRepository, IMapper mapper, IEventService eventService, IEventRepository eventRepository, IHostingEnvironment hostingEnvironment)
        {
            _spotRepository = spotRepository;
            _mapper = mapper;
            _eventRepository = eventRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task AddSpot(CreateSpotViewModel model)
        {
            //var s = _mapper.Map<SpotViewModel, Spot>(model);
            //s.Id = _spotRepository.GetLastId() + 1;
            //_spotRepository.Add(s);
            //await _spotRepository.CommitAsync();
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


        public async Task Create(CreateSpotViewModel model)
        {
            //var spot = _mapper.Map<Spot>(model);
            var coords = GetCoords(model.Coords);
            var spot = new Spot
            {
                Name = model.Name,
                Description = model.Description,
                Latitude = coords.Latitude,
                Longitude = coords.Longitude
            };

            _spotRepository.Add(spot);

            await _spotRepository.CommitAsync();
            spot.Image = UploadSpotPhoto(model.SpotImage, spot.Id);
            _spotRepository.Update(spot);
            await _spotRepository.CommitAsync();




        }

        public async Task<List<Spot>> GetAllSpots() => (await _spotRepository.GetAllAsync()).ToList();

        public string UploadSpotPhoto(IFormFile formFile, int spotId)
        {
            string newPath = Path.Combine(_hostingEnvironment.WebRootPath, "Spots", spotId.ToString());
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            if (!formFile.ContentType.Contains("image"))
            {
                throw new Exception("Tylko obrazy");
            }
            FileInfo uploadedFileInfo = new FileInfo(formFile.FileName);
            var fileName = $"{Guid.NewGuid()}{uploadedFileInfo.Extension}";
            using (var stream = new FileStream(Path.Combine(newPath, fileName), FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return fileName;


        }

        private Coords GetCoords(string coordinates)
        {
            coordinates = coordinates.Replace("(", "");
            coordinates = coordinates.Replace(")", "");

            var latlng = coordinates.Split(",") ?? throw new ArgumentNullException(nameof(coordinates));

            return new Coords
            {
                Latitude = Convert.ToDouble(latlng[0], CultureInfo.InvariantCulture.NumberFormat),
                Longitude = Convert.ToDouble(latlng[1], CultureInfo.InvariantCulture.NumberFormat)
            };
        }
    }
}
