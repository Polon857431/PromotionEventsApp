using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.ViewModels
{
    public class SpotViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public List<Event> Events { get; set; }
    }
}
