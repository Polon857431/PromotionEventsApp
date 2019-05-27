using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.ViewModels
{
    public class UserEventsViewModel
    {
        public Event Event { get; set; }
        public List<Spot> EventSpots { get; set; }
        public int Points { get; set; }
    }
}
