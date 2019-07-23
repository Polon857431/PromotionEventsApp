using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.ViewModels
{
    public class SpotViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public List<Event> Events { get; set; }
        public IFormFile SpotImage { get; set; }
    }
}
