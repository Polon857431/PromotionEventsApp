using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.Models
{
    public class Spot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public ICollection<EventSpot> Events { get; set; }
    }
}
