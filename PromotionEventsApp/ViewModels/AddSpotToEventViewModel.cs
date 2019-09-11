using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.ViewModels
{
    public class AddSpotToEventViewModel
    {
        public int EventId { get; set; }
        public List<Spot> EventSpots { get; set; }
        public List<Spot> AvailableSpots { get; set; }
    }
}
